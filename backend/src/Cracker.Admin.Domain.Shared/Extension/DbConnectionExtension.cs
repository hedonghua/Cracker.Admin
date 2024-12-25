using System.Data;
using System.Linq;
using System.Threading.Tasks;

using Dapper;

using Cracker.Admin.Models;

namespace Cracker.Admin.Extension
{
    public static class DbConnectionExtension
    {
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="TDto"></typeparam>
        /// <param name="connection"></param>
        /// <param name="sql"></param>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static async Task<PagedResultStruct<TDto>> GetPagedAsync<TDto>(this IDbConnection connection, string sql, int page, int size, object? param = default)
        {
            if (connection.State != ConnectionState.Open) connection.Open();
            int count = await connection.ExecuteScalarAsync<int>($"select count(*) from ({sql}) m", param);
            sql += $" limit {size} offset {(page - 1) * size} ";
            var list = (await connection.QueryAsync<TDto>(sql, param)).ToList();
            return new PagedResultStruct<TDto>(count,list);
        }
    }
}