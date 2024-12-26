using System;
using System.Threading.Tasks;
using Cracker.Admin.Database.DAO;
using Cracker.Admin.Entities;
using Cracker.Admin.EntitiesFrameworkCore;

namespace Cracker.Admin.DAO
{
    public class LoginLogDAO : ILoginLogDAO
    {
        private readonly CrackerAdminDbContext _context;

        public LoginLogDAO(CrackerAdminDbContext context)
        {
            _context = context;
        }

        public async Task<int> WriteAsync(SysLoginLog entity)
        {
            entity.SetCreationTime();
            _context.Add(entity);
            return await _context.SaveChangesAsync();
        }
    }
}