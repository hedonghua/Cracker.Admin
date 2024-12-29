using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Cracker.Admin.Entities;
using Cracker.Admin.Enums;
using Cracker.Admin.Helpers;
using Cracker.Admin.Repositories;

using Scriban;

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
        public async Task<string> BuildEntity(GenTable genTable)
        {
            var columns = await genTableColumnRepository.GetListAsync(x => x.GenTableId == genTable.Id);
            var template = Template.Parse(await ReadTemplateFileAsync("Entity"));
            return template.Render(new { Columns = columns });
        }

        private async Task<string> ReadTemplateFileAsync(string name)
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Developer", "Templates", name + ".sc");
            return await File.ReadAllTextAsync(filePath);
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
    }
}