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
    public DbSet<OrgPosition> OrgPosition => Set<OrgPosition>();
    public DbSet<OrgPositionGroup> OrgPositionGroup => Set<OrgPositionGroup>();
    public DbSet<OrgEmployee> OrgEmployee => Set<OrgEmployee>();

    public DbSet<SysConfig> SysConfig => Set<SysConfig>();

    public DbSet<SysTenant> SysTenant => Set<SysTenant>();
    public DbSet<SysRoleDept> SysRoleDept => Set<SysRoleDept>();
    public DbSet<Notification> Notification => Set<Notification>();

    public CrackerAdminDbContext(DbContextOptions<CrackerAdminDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<SysUserRole>().HasKey(x => new { x.UserId, x.RoleId });
        builder.Entity<SysRoleMenu>().HasKey(x => new { x.RoleId, x.MenuId });
        builder.Entity<SysDictType>().HasAlternateKey(x => x.DictType);
        builder.Entity<SysDictData>()
            .HasOne(x => x.SysDictType)
            .WithMany(x => x.DictDatas)
            .HasForeignKey(x => x.DictType)
            .HasPrincipalKey(x => x.DictType);
        builder.Entity<SysRoleDept>().HasKey(x => new { x.RoleId, x.DeptId });
    }
}