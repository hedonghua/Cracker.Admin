using Cracker.Admin.Entities;

using Microsoft.EntityFrameworkCore;

using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Cracker.Admin;

[ConnectionStringName("Default")]
public class CrackerAdminDbContext :
    AbpDbContext<CrackerAdminDbContext>
{
    #region 系统

    public DbSet<SysUser> SysUser { get; }
    public DbSet<SysRole> SysRole { get; }
    public DbSet<SysMenu> SysMenu { get; }
    public DbSet<SysUserRole> SysUserRole { get; }
    public DbSet<SysRoleMenu> SysRoleMenu { get; }
    public DbSet<SysDict> SysDict { get; }
    public DbSet<SysBusinessLog> SysBusinessLog { get; }
    public DbSet<SysLoginLog> SysLoginLog { get; }

    #endregion 系统

    #region 组织架构

    public DbSet<OrgDept> OrgDept { get; }
    public DbSet<OrgDeptEmployee> OrgDeptEmployee { get; }
    public DbSet<OrgPosition> OrgPosition { get; }
    public DbSet<OrgPositionGroup> OrgPositionGroup { get; }
    public DbSet<OrgEmployee> OrgEmployee { get; }

    #endregion 组织架构

    #region 代码生成

    public DbSet<GenTable> GenTable { get; }
    public DbSet<GenTableColumn> GenTableColumn { get; }

    #endregion 代码生成

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
        builder.Entity<SysDict>().HasKey(x => x.Id);
        builder.Entity<SysBusinessLog>().HasKey(x => x.Id);
        builder.Entity<SysLoginLog>().HasKey(x => x.Id);

        builder.Entity<OrgDept>().HasKey(x => x.Id);
        builder.Entity<OrgDeptEmployee>().HasKey(x => x.Id);
        builder.Entity<OrgPosition>().HasKey(x => x.Id);
        builder.Entity<OrgPositionGroup>().HasKey(x => x.Id);
        builder.Entity<OrgEmployee>().HasKey(x => x.Id);

        builder.Entity<GenTable>().HasKey(x => x.Id);
        builder.Entity<GenTableColumn>().HasKey(x => x.Id);
    }
}