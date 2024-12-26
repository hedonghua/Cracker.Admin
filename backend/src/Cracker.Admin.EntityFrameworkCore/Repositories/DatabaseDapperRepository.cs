using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Cracker.Admin.Models;

using Dapper;

using Volo.Abp.Domain.Repositories.Dapper;
using Volo.Abp.EntityFrameworkCore;

namespace Cracker.Admin.Repositories
{
    public class DatabaseDapperRepository : DapperRepository<CrackerAdminDbContext>, IDatabaseDapperRepository
    {
        public DatabaseDapperRepository(IDbContextProvider<CrackerAdminDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<List<DatabaseTableColumn>> GetDatabaseTableColumnsAsync(string tableName)
        {
            var connection = await GetDbConnectionAsync();
            var sql = @"SELECT
	                A.TABLE_NAME 'TableName',
	                A.COLUMN_NAME 'ColumnName',
	                A.DATA_TYPE 'ColumnType',
	                A.COLUMN_COMMENT 'Comment',
	                A.CHARACTER_MAXIMUM_LENGTH 'MaxLength',
	                A.IS_NULLABLE 'IsNullable'
                FROM
	                INFORMATION_SCHEMA.COLUMNS A
                WHERE
	                A.TABLE_SCHEMA = @database
	                AND A.TABLE_NAME = @tableName
                ORDER BY
	                A.TABLE_SCHEMA,
	                A.TABLE_NAME,
	                A.ORDINAL_POSITION";

            return (await connection.QueryAsync<DatabaseTableColumn>(sql, new { database = connection.Database, tableName }, await GetDbTransactionAsync())).ToList();
        }

        public async Task<List<DatabaseTable>> GetDatabaseTablesAsync(int page, int size, RefAsync<int> total, string? tableName = default)
        {
            var connection = await GetDbConnectionAsync();
            var sql = @"SELECT
	                A.TABLE_NAME AS 'TableName',
	                A.TABLE_ROWS AS 'Rows',
	                A.CREATE_TIME AS 'CreateTime',
	                A.TABLE_COMMENT 'Comment'
                FROM
	                INFORMATION_SCHEMA.TABLES A
                WHERE
	                A.TABLE_SCHEMA = @database";

            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@database", connection.Database);
            if (!string.IsNullOrEmpty(tableName))
            {
                sql += " and A.TABLE_NAME like @tableName ";
                dynamicParameters.Add("@tableName", "%" + tableName + "%");
            }

            total.Value = await connection.ExecuteScalarAsync<int>($"select count(*) from ({sql}) m", dynamicParameters);
            sql += $" ORDER BY A.TABLE_NAME limit {size} offset {(page - 1) * size} ";
            return (await connection.QueryAsync<DatabaseTable>(sql, dynamicParameters, await GetDbTransactionAsync())).ToList();
        }

        public async Task<bool> HasColumnAsync(string tableName, string columnName)
        {
            var connection = await GetDbConnectionAsync();
            var sql = "select count(*) from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME=@tableName and TABLE_SCHEMA=@database and COLUMN_NAME=@columnName";
            return 0 < await connection.ExecuteScalarAsync<long>(sql, new { database = connection.Database, tableName, columnName }, await GetDbTransactionAsync());
        }

        public async Task<bool> HasTableAsync(string tableName)
        {
            var connection = await GetDbConnectionAsync();
            var sql = "select count(*) from INFORMATION_SCHEMA.TABLES where TABLE_NAME=@tableName and TABLE_SCHEMA=@database";
            return 0 < await connection.ExecuteScalarAsync<long>(sql, new { database = connection.Database, tableName }, await GetDbTransactionAsync());
        }
    }
}