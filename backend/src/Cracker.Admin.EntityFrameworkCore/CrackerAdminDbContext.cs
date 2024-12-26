using System;
using System.Linq;
using System.Reflection;

using Microsoft.EntityFrameworkCore;

using Cracker.Admin.Entity;

using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Cracker.Admin.EntityFrameworkCore;

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
    public DbSet<SysFile> SysFile { get; }
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

#pragma warning disable CS8618 // 在退出构造函数时，不可为 null 的字段必须包含非 null 值。请考虑声明为可以为 null。

    public CrackerAdminDbContext(DbContextOptions<CrackerAdminDbContext> options)
#pragma warning restore CS8618 // 在退出构造函数时，不可为 null 的字段必须包含非 null 值。请考虑声明为可以为 null。
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        var entityConfigurationType = typeof(IEntityTypeConfiguration<>);
        var typesToRegister = Assembly.GetExecutingAssembly().DefinedTypes.Where(q => q.GetInterface(entityConfigurationType.FullName!) != null);
        foreach (var type in typesToRegister)
        {
            dynamic? configurationInstance = Activator.CreateInstance(type);
            if (configurationInstance == null) continue;
            builder.ApplyConfiguration(configurationInstance);
        }
    }
}