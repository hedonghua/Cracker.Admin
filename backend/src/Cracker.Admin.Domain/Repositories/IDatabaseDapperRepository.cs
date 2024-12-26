using System.Collections.Generic;
using System.Threading.Tasks;

using Cracker.Admin.Models;

namespace Cracker.Admin.Repositories
{
    public interface IDatabaseDapperRepository
    {
        Task<bool> HasTableAsync(string tableName);

        Task<bool> HasColumnAsync(string tableName, string columnName);

        Task<List<DatabaseTable>> GetDatabaseTablesAsync(int page, int size, RefAsync<int> total, string? tableName = default);

        Task<List<DatabaseTableColumn>> GetDatabaseTableColumnsAsync(string tableName);
    }
}