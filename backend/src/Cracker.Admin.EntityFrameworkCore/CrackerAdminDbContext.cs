using Cracker.Admin.Entities;

using Microsoft.EntityFrameworkCore;

using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Cracker.Admin;

[ConnectionStringName("Default")]
public class CrackerAdminDbContext :
    AbpDbContext<CrackerAdminDbContext>
{
    public DbSet<SysUser> SysUser => Set<SysUser>();
    public DbSet<SysRole> SysRole => Set<SysRole>();
    public DbSet<SysMenu> SysMenu => Set<SysMenu>();
    public DbSet<SysUserRole> SysUserRole => Set<SysUserRole>();
    public DbSet<SysRoleMenu> SysRoleMenu => Set<SysRoleMenu>();
    public DbSet<SysDictData> SysDictData => Set<SysDictData>();
    public DbSet<SysDictType> SysDictType => Set<SysDictType>();
    public DbSet<SysBusinessLog> SysBusinessLog => Set<SysBusinessLog>();
    public DbSet<SysLoginLog> SysLoginLog => Set<SysLoginLog>();

    public DbSet<OrgDept> OrgDept => Set<OrgDept>();
    public DbSet<OrgDeptEmployee> OrgDeptEmployee => Set<OrgDeptEmployee>();
    public DbSet<OrgPosition> OrgPosition => Set<OrgPosition>();
    public DbSet<OrgPositionGroup> OrgPositionGroup => Set<OrgPositionGroup>();
    public DbSet<OrgEmployee> OrgEmployee => Set<OrgEmployee>();

    public DbSet<GenTable> GenTable => Set<GenTable>();
    public DbSet<GenTableColumn> GenTableColumn => Set<GenTableColumn>();

    public DbSet<SysConfig> SysConfig => Set<SysConfig>();

    public CrackerAdminDbContext(DbContextOptions<CrackerAdminDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<SysUser>().HasKey(x => x.Id);
        builder.Entity<SysRole>().HasKey(x => x.Id);
        builder.Entity<SysMenu>().HasKey(x => x.Id);
        builder.Entity<SysUserRole>().HasKey(x => new { x.UserId, x.RoleId });
        builder.Entity<SysRoleMenu>().HasKey(x => new { x.RoleId, x.MenuId });
        builder.Entity<SysDictData>().HasKey(x => x.Id);
        builder.Entity<SysDictType>().HasKey(x => x.Id);
        builder.Entity<SysBusinessLog>().HasKey(x => x.Id);
        builder.Entity<SysLoginLog>().HasKey(x => x.Id);

        builder.Entity<OrgDept>().HasKey(x => x.Id);
        builder.Entity<OrgDeptEmployee>().HasKey(x => x.Id);
        builder.Entity<OrgPosition>().HasKey(x => x.Id);
        builder.Entity<OrgPositionGroup>().HasKey(x => x.Id);
        builder.Entity<OrgEmployee>().HasKey(x => x.Id);

        builder.Entity<GenTable>().HasKey(x => x.Id);
        builder.Entity<GenTableColumn>().HasKey(x => x.Id);

        builder.Entity<SysConfig>().HasKey(x => x.Id);
    }
}