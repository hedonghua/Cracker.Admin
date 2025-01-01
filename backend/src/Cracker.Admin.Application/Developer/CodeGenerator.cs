using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Cracker.Admin.Entities;
using Cracker.Admin.Enums;
using Cracker.Admin.Helpers;
using Cracker.Admin.Repositories;

using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Domain.Repositories;

namespace Cracker.Admin.Developer
{
    public class CodeGenerator : ITransientDependency
    {
        private readonly IDatabaseDapperRepository databaseDapperRepository;
        private readonly IRepository<GenTableColumn> genTableColumnRepository;
        private readonly IRepository<GenTable> genTableRepository;

        public CodeGenerator(IDatabaseDapperRepository databaseDapperRepository, IRepository<GenTableColumn> genTableColumnRepository
            , IRepository<GenTable> genTableRepository)
        {
            this.databaseDapperRepository = databaseDapperRepository;
            this.genTableColumnRepository = genTableColumnRepository;
            this.genTableRepository = genTableRepository;
        }

        public async Task InitGenTableColumnAsync(string tableName)
        {
            var genTable = await genTableRepository.GetAsync(x => x.TableName == tableName);

            var columns = await databaseDapperRepository.GetDatabaseTableColumnsAsync(tableName);
            if (columns.Count <= 0) return;

            var list = new List<GenTableColumn>();
            foreach (var column in columns)
            {
                var isAuditProp = this.IsAuditProp(column.ColumnName);
                var item = new GenTableColumn()
                {
                    GenTableId = genTable.Id,
                    TableName = tableName,
                    ColumnName = column.ColumnName,
                    ColumnType = column.ColumnType,
                    HtmlType = this.GetHtmlType(column.ColumnType),
                    Comment = column.Comment,
                    MaxLength = column.MaxLength,
                    IsNullable = column.IsNullable == "YES",
                    IsInsert = !isAuditProp,
                    IsUpdate = !isAuditProp,
                    IsShow = !isAuditProp,
                    IsSearch = this.IsDefaultSearch(column.ColumnName),
                    SearchType = SearchType.Contains,
                    CsharpPropName = StringHelper.ToPascalCase(column.ColumnName)
                };
                item.JsFieldName = item.CsharpPropName[..1].ToLower() + item.CsharpPropName[1..];
                item.CsharpType = this.GetCsharpType(column.ColumnType, item.IsNullable, column.MaxLength ?? 0);
                item.JsType = this.GetJsType(column.ColumnType, item.IsNullable);
                list.Add(item);
            }

            await genTableColumnRepository.DeleteDirectAsync(x => x.GenTableId == genTable.Id);
            await genTableColumnRepository.InsertManyAsync(list);
        }

        /// <summary>
        /// 生成实体类
        /// </summary>
        /// <param name="genTable"></param>
        /// <returns></returns>
        public async Task<(string, string)> BuildEntity(GenTable genTable, List<GenTableColumn> columns)
        {
            var template = await ReadTemplateFileAsync("Entity");

            var stringBuilder = new StringBuilder();
            var index = 1;
            foreach (var column in columns)
            {
                var hasComment = !string.IsNullOrEmpty(column.Comment);
                if (hasComment)
                {
                    stringBuilder.AppendLine("\t/// <summary>");
                    stringBuilder.AppendLine($"\t/// {column.Comment}");
                    stringBuilder.AppendLine("\t/// </summary>");
                }
                if (!column.IsNullable)
                {
                    stringBuilder.AppendLine("\t[NotNull]");
                    stringBuilder.AppendLine("\t[Required]");
                }
                if (column.CsharpType == "string")
                {
                    stringBuilder.AppendLine($"\t[StringLength({column.MaxLength})]");
                }
                if (hasComment)
                {
                    stringBuilder.AppendLine($"\t[Comment(\"{column.Comment}\")]");
                }
                if (column.CsharpPropName != column.ColumnName)
                {
                    stringBuilder.AppendLine($"\t[Column(\"{column.ColumnName}\")]");
                }
                if (index < columns.Count)
                {
                    stringBuilder.AppendLine($"\tpublic {column.CsharpType} {column.CsharpPropName} {{ get; set; }}\r\n");
                }
                else
                {
                    stringBuilder.Append($"\tpublic {column.CsharpType} {column.CsharpPropName} {{ get; set; }}");
                }
                index++;
            }

            var source = this.RenderTemplate(template, new
            {
                tableName = genTable.TableName,
                comment = genTable.Comment,
                entityName = genTable.EntityName,
                moduleName = genTable.ModuleName,
                properties = stringBuilder.ToString(),
                baseClass = ""
            });

            return (genTable.EntityName, source);
        }

        public async Task<(string, string)> BuildIService(GenTable genTable, List<GenTableColumn> columns)
        {
            var template = await ReadTemplateFileAsync("IService");

            var source = this.RenderTemplate(template, new
            {
                moduleName = genTable.ModuleName,
                businessName = genTable.BusinessName,
                businessNameOfFirstLower = char.ToLower(genTable.BusinessName[0]) + genTable.BusinessName[1..],
            });

            return ($"I{genTable.BusinessName}Service", source);
        }

        public async Task<(string, string)> BuildService(GenTable genTable, List<GenTableColumn> columns)
        {
            var template = await ReadTemplateFileAsync("Service");

            //新增字段
            var addFields = columns.Where(x => x.IsInsert).ToList();
            var addFieldsStringbuilder = new StringBuilder();
            int j = 0;
            foreach (var item in addFields)
            {
                j++;
                if (j == 1)
                {
                    addFieldsStringbuilder.AppendLine($"entity.{item.CsharpPropName} = dto.{item.CsharpPropName};");
                    continue;
                }
                addFieldsStringbuilder.AppendLine($"        entity.{item.CsharpPropName} = dto.{item.CsharpPropName};");
            }
            //修改字段
            var updateFields = columns.Where(x => x.IsUpdate).ToList();
            var updateFieldsStringbuilder = new StringBuilder();
            int e = 0;
            foreach (var item in updateFields)
            {
                e++;
                if (e == 1)
                {
                    updateFieldsStringbuilder.AppendLine($"entity.{item.CsharpPropName} = dto.{item.CsharpPropName};");
                    continue;
                }
                updateFieldsStringbuilder.AppendLine($"        entity.{item.CsharpPropName} = dto.{item.CsharpPropName};");
            }
            //查询列
            var queryColumns = columns.Where(x => x.IsShow).ToList();
            var queryColumnsStringbuilder = new StringBuilder();
            int i = 0;
            foreach (var item in queryColumns)
            {
                i++;
                var comma = i < queryColumns.Count ? "," : "";
                if (i == 1)
                {
                    queryColumnsStringbuilder.AppendLine($"{item.CsharpPropName} = x.{item.CsharpPropName}{comma}");
                    continue;
                }
                queryColumnsStringbuilder.AppendLine($"                {item.CsharpPropName} = x.{item.CsharpPropName}{comma}");
            }
            //查询条件
            var queryConditions = columns.Where(x => x.IsSearch).ToList();
            var queryConditionsStringBuilder = new StringBuilder();
            foreach (var item in queryConditions)
            {
                if (item.CsharpType == "string")
                {
                    if (item.SearchType == SearchType.Contains)
                    {
                        queryConditionsStringBuilder.AppendLine($".WhereIf(!string.IsNullOrEmpty(dto.{item.CsharpPropName}), x => x.{item.CsharpPropName}.Contains(dto.{item.CsharpPropName}))");
                    }
                    else if (item.SearchType == SearchType.Equals)
                    {
                        queryConditionsStringBuilder.AppendLine($".WhereIf(!string.IsNullOrEmpty(dto.{item.CsharpPropName}), x => x.{item.CsharpPropName} == dto.{item.CsharpPropName})");
                    }
                }
            }

            var source = this.RenderTemplate(template, new
            {
                moduleName = genTable.ModuleName,
                businessName = genTable.BusinessName,
                businessNameOfFirstLower = char.ToLower(genTable.BusinessName[0]) + genTable.BusinessName[1..],
                entityName = genTable.EntityName,
                addFields = addFieldsStringbuilder.ToString(),
                updateFields = updateFieldsStringbuilder.ToString(),
                queryConditions = queryConditionsStringBuilder.ToString().TrimEnd('\r', '\n'),
                queryMapper = queryColumnsStringbuilder.ToString().TrimEnd('\r', '\n'),
            });

            return ($"{genTable.BusinessName}Service", source);
        }

        public async Task<(string, string)> BuildEntityDto(GenTable genTable, List<GenTableColumn> columns, int type)
        {
            //type:1 dto,2 searchDto,3 resultDto
            var dtoName = $"{genTable.BusinessName}";
            var baseClass = string.Empty;
            if (type == 1)
            {
                dtoName += "Dto";
            }
            else if (type == 2)
            {
                dtoName += "SearchDto";
                baseClass = ": PageSearch";
            }
            else
            {
                dtoName += "ResultDto";
            }

            var stringBuilder = new StringBuilder();
            foreach (var column in columns)
            {
                if (type == 1 && !(column.IsInsert || column.IsUpdate)) continue;
                if (type == 2 && !column.IsSearch) continue;
                if (type == 3 && !column.IsShow) continue;

                var hasComment = !string.IsNullOrEmpty(column.Comment);
                if (hasComment)
                {
                    stringBuilder.AppendLine("\t/// <summary>");
                    stringBuilder.AppendLine($"\t/// {column.Comment}");
                    stringBuilder.AppendLine("\t/// </summary>");
                }
                if (!column.IsNullable)
                {
                    stringBuilder.AppendLine("\t[NotNull]");
                    stringBuilder.AppendLine("\t[Required]");
                }
                if (column.CsharpType == "string")
                {
                    stringBuilder.AppendLine($"\t[StringLength({column.MaxLength})]");
                }
                stringBuilder.AppendLine($"\tpublic {column.CsharpType} {column.CsharpPropName} {{ get; set; }}\r\n");
            }

            var template = await ReadTemplateFileAsync("EntityDto");
            var source = this.RenderTemplate(template, new
            {
                dtoName = dtoName,
                baseClass = baseClass,
                moduleName = genTable.ModuleName,
                properties = stringBuilder.ToString().TrimEnd('\r', '\n'),
            });

            return ($"{dtoName}", source);
        }

        public async Task<(string, string)> BuildController(GenTable genTable, List<GenTableColumn> columns)
        {
            var template = await ReadTemplateFileAsync("Controller");

            var source = this.RenderTemplate(template, new
            {
                moduleName = genTable.ModuleName,
                businessName = genTable.BusinessName,
                businessNameOfFirstLower = char.ToLower(genTable.BusinessName[0]) + genTable.BusinessName[1..],
            });

            return ($"{genTable.BusinessName}Controller", source);
        }

        private async Task<string> ReadTemplateFileAsync(string name)
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Developer", "Templates", name + ".sc");
            return await File.ReadAllTextAsync(filePath,Encoding.UTF8);
        }

        private string GetCsharpType(string columnType, bool isNullable, long maxLen)
        {
            var type = FindType();
            return isNullable ? type + "?" : type;

            string FindType()
            {
                switch (columnType)
                {
                    case "tinyint":
                        return "sbyte";

                    case "smallint":
                        return "short";

                    case "mediumint":
                    case "int":
                        return "int";

                    case "bigint":
                        return "long";

                    case "float":
                        return "float";

                    case "double":
                        return "double";

                    case "decimal":
                        return "decimal";

                    case "date":
                    case "datetime":
                    case "timestamp":
                        return "DateTime";

                    case "time":
                        return "TimeSpan";

                    case "year":
                        return "short";

                    case "char":
                        if (maxLen == 36) return "Guid";
                        return "string";

                    case "varchar":
                    case "text":
                    case "enum":
                    case "set":
                        return "string";

                    case "blob":
                        return "byte[]";

                    default:
                        return "string";
                }
            }
        }

        private string GetJsType(string columnType, bool isNullable)
        {
            var type = FindType();
            return isNullable ? type + "?" : type;

            string FindType()
            {
                switch (columnType)
                {
                    case "tinyint":
                    case "smallint":
                    case "mediumint":
                    case "int":
                    case "bigint":
                        return "number";

                    case "float":
                    case "double":
                    case "decimal":
                        return "number"; // 在JavaScript中，所有数字类型都使用 number
                    case "date":
                    case "datetime":
                    case "timestamp":
                        return "Date";

                    case "time":
                        return "string"; // JavaScript中没有内置的Time类型，可以用字符串表示
                    case "year":
                        return "number";

                    case "char":
                    case "varchar":
                    case "text":
                    case "enum":
                    case "set":
                        return "string";

                    case "blob":
                        return "ArrayBuffer"; // 或者 "Blob" 或 "Buffer"（在Node.js中）
                    default:
                        return "string";
                }
            }
        }

        private string GetHtmlType(string columnType)
        {
            return columnType switch
            {
                "date" => "date",
                "datetime" => "datetime",
                _ => "text"
            };
        }

        private bool IsAuditProp(string columnName)
        {
            string[] props = [nameof(FullAuditedAggregateRoot.CreationTime), nameof(FullAuditedAggregateRoot.CreatorId),nameof(FullAuditedAggregateRoot.ConcurrencyStamp)
                , nameof(FullAuditedAggregateRoot.LastModificationTime),nameof(FullAuditedAggregateRoot.LastModifierId),nameof(FullAuditedAggregateRoot.ExtraProperties)
                ,nameof(FullAuditedAggregateRoot.IsDeleted),nameof(FullAuditedAggregateRoot.DeletionTime),nameof(FullAuditedAggregateRoot.DeleterId)];
            return props.Contains(columnName);
        }

        private bool IsDefaultSearch(string columnName)
        {
            if (IsAuditProp(columnName)) return false;
            return Regex.IsMatch(columnName, @"Status|Name|Title|Reason");
        }

        /// <summary>
        /// 渲染模板
        /// </summary>
        /// <param name="obj">obj下属性都当string解析</param>
        /// <returns></returns>
        private string RenderTemplate(string template, dynamic obj)
        {
            // 将dynamic对象转换为object，以便使用反射
            object objAsObject = obj;

            // 获取对象的类型
            Type type = objAsObject.GetType();

            // 获取并输出所有属性
            foreach (PropertyInfo property in type.GetProperties())
            {
                string propName = property.Name;
                string pattern = @"\{\{\s*" + Regex.Escape(propName) + @"\s*\}\}";
                template = Regex.Replace(template, pattern, (string)property.GetValue(objAsObject)!);
            }

            return template;
        }
    }
}