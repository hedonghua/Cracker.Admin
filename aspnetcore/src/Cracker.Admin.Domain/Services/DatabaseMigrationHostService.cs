using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Hosting;

using Volo.Abp.Data;

namespace Cracker.Admin.Services
{
    public class DatabaseMigrationHostService : BackgroundService
    {
        private readonly IDataSeeder dataSeeder;

        public DatabaseMigrationHostService(IDataSeeder dataSeeder)
        {
            this.dataSeeder = dataSeeder;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await dataSeeder.SeedAsync();
        }
    }
}