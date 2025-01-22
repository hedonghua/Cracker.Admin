using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cracker.Admin.Migrations
{
    /// <inheritdoc />
    public partial class InitCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "gen_table",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TableName = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false, comment: "表名")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Comment = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, comment: "表描述")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BusinessName = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false, comment: "业务名")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EntityName = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false, comment: "实体名")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ModuleName = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false, comment: "模块名")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Types = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, comment: "实现接口/继承抽象类")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreationTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatorId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    LastModificationTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gen_table", x => x.Id);
                },
                comment: "生成表")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "org_dept",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Code = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false, comment: "部门编号")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false, comment: "部门名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Sort = table.Column<int>(type: "int", nullable: false, comment: "排序"),
                    Description = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: true, comment: "描述")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<int>(type: "int", nullable: false, comment: "状态：1正常2停用"),
                    CuratorId = table.Column<Guid>(type: "char(36)", nullable: true, comment: "负责人", collation: "ascii_general_ci"),
                    Email = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true, comment: "邮箱")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Phone = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true, comment: "电话")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ParentId = table.Column<Guid>(type: "char(36)", nullable: true, comment: "父ID", collation: "ascii_general_ci"),
                    ParentIds = table.Column<string>(type: "varchar(1024)", maxLength: 1024, nullable: true, comment: "层级父ID")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Layer = table.Column<int>(type: "int", nullable: false, comment: "层级"),
                    ExtraProperties = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ConcurrencyStamp = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreationTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatorId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    LastModificationTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    DeletionTime = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_org_dept", x => x.Id);
                },
                comment: "部门表")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "org_dept_employee",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    EmployeeId = table.Column<Guid>(type: "char(36)", nullable: false, comment: "员工ID", collation: "ascii_general_ci"),
                    DeptId = table.Column<Guid>(type: "char(36)", nullable: false, comment: "部门ID", collation: "ascii_general_ci"),
                    PositionId = table.Column<Guid>(type: "char(36)", nullable: false, comment: "职位ID", collation: "ascii_general_ci"),
                    IsMain = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否主职位"),
                    ExtraProperties = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ConcurrencyStamp = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreationTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatorId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    LastModificationTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    DeletionTime = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_org_dept_employee", x => x.Id);
                },
                comment: "员工关联部门")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "org_employee",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Code = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false, comment: "工号")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false, comment: "姓名")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Sex = table.Column<int>(type: "int", nullable: false, comment: "性别"),
                    Phone = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: false, comment: "手机号码")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IdNo = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false, comment: "身份证")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FrontIdNoUrl = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: true, comment: "身份证正面")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BackIdNoUrl = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: true, comment: "身份证背面")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BirthDay = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "生日"),
                    Address = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: true, comment: "现住址")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true, comment: "邮箱")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    InTime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "入职时间"),
                    OutTime = table.Column<DateTime>(type: "datetime(6)", nullable: true, comment: "离职时间"),
                    IsOut = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否离职"),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: true, comment: "关联用户ID", collation: "ascii_general_ci"),
                    DeptId = table.Column<Guid>(type: "char(36)", nullable: false, comment: "部门ID", collation: "ascii_general_ci"),
                    PositionId = table.Column<Guid>(type: "char(36)", nullable: false, comment: "职位ID", collation: "ascii_general_ci"),
                    ExtraProperties = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ConcurrencyStamp = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreationTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatorId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    LastModificationTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    DeletionTime = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_org_employee", x => x.Id);
                },
                comment: "员工表")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "org_position",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Code = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false, comment: "职位编号")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false, comment: "职位名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Level = table.Column<int>(type: "int", nullable: false, comment: "职级"),
                    Status = table.Column<int>(type: "int", nullable: false, comment: "状态：1正常2停用"),
                    Description = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: true, comment: "描述")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    GroupId = table.Column<Guid>(type: "char(36)", nullable: true, comment: "职位分组", collation: "ascii_general_ci"),
                    ExtraProperties = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ConcurrencyStamp = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreationTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatorId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    LastModificationTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    DeletionTime = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_org_position", x => x.Id);
                },
                comment: "职位表")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "org_positiongroup",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    GroupName = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false, comment: "分组名")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Remark = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: true, comment: "备注")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ParentId = table.Column<Guid>(type: "char(36)", nullable: true, comment: "父ID", collation: "ascii_general_ci"),
                    ParentIds = table.Column<string>(type: "varchar(1024)", maxLength: 1024, nullable: true, comment: "层级父ID")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreationTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatorId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    LastModificationTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_org_positiongroup", x => x.Id);
                },
                comment: "职位分组")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sys_businesslog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false, comment: "账号")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Action = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: false, comment: "操作方法，全名")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HttpMethod = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: false, comment: "http请求方式")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Url = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: false, comment: "请求地址")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Os = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true, comment: "系统")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Browser = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true, comment: "浏览器")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NodeName = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false, comment: "操作节点名")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Ip = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true, comment: "IP")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Address = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, comment: "登录地址")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsSuccess = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否成功"),
                    OperationMsg = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true, comment: "操作信息")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MillSeconds = table.Column<int>(type: "int", nullable: false, comment: "耗时，单位毫秒"),
                    RequestId = table.Column<string>(type: "longtext", nullable: true, comment: "请求跟踪ID")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreationTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatorId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sys_businesslog", x => x.Id);
                },
                comment: "业务日志")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sys_config",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false, comment: "配置名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Key = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false, comment: "配置键名")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Value = table.Column<string>(type: "longtext", nullable: false, comment: "配置键值")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    GroupKey = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Remark = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: true, comment: "备注")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreationTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatorId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    LastModificationTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sys_config", x => x.Id);
                },
                comment: "系统配置")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sys_dict_data",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Key = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Value = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Label = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DictType = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Remark = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Sort = table.Column<int>(type: "int", nullable: false),
                    IsEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatorId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    LastModificationTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sys_dict_data", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sys_dict_type",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DictType = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Remark = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatorId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    LastModificationTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sys_dict_type", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sys_loginlog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false, comment: "账号")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Ip = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true, comment: "IP")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Address = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, comment: "登录地址")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Os = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true, comment: "系统")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Browser = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true, comment: "浏览器")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OperationMsg = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true, comment: "浏览器操作信息")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsSuccess = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否成功"),
                    SessionId = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: true, comment: "会话ID")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreationTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatorId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sys_loginlog", x => x.Id);
                },
                comment: "登录日志")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sys_menu",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Title = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false, comment: "显示标题/名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true, comment: "组件名")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Icon = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true, comment: "图标")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Path = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, comment: "路由/地址")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FunctionType = table.Column<int>(type: "int", nullable: false, comment: "功能类型"),
                    Permission = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true, comment: "授权码")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ParentId = table.Column<Guid>(type: "char(36)", nullable: true, comment: "父级ID", collation: "ascii_general_ci"),
                    Sort = table.Column<int>(type: "int", nullable: false, comment: "排序"),
                    Hidden = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否隐藏"),
                    ExtraProperties = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ConcurrencyStamp = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreationTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatorId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    LastModificationTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    DeletionTime = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sys_menu", x => x.Id);
                },
                comment: "菜单表")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sys_role",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    RoleName = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false, comment: "角色名")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Remark = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: true, comment: "备注")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ExtraProperties = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ConcurrencyStamp = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreationTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatorId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    LastModificationTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    DeletionTime = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sys_role", x => x.Id);
                },
                comment: "角色表")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sys_user",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserName = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false, comment: "用户名")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: false, comment: "密码")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PasswordSalt = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false, comment: "密码盐")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Avatar = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, comment: "头像")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NickName = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false, comment: "昵称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Sex = table.Column<int>(type: "int", nullable: false, comment: "性别"),
                    IsEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否启用"),
                    ExtraProperties = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ConcurrencyStamp = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreationTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatorId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    LastModificationTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    DeletionTime = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sys_user", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SysTenant",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false, comment: "租户名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ConnectionString = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: false, comment: "连接字符串")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Remark = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: true, comment: "备注")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreationTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatorId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    LastModificationTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysTenant", x => x.Id);
                },
                comment: "租户表")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "gen_table_column",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    GenTableId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TableName = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false, comment: "表名")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ColumnName = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false, comment: "列名")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CsharpPropName = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false, comment: "C#属性名")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    JsFieldName = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false, comment: "JS字段名")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ColumnType = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false, comment: "数据库列类型")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CsharpType = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true, comment: "C#类型")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    JsType = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true, comment: "JS类型")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HtmlType = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true, comment: "HTML类型")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Comment = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, comment: "列描述")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MaxLength = table.Column<long>(type: "bigint", nullable: true, comment: "最大长度"),
                    IsNullable = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否可空"),
                    IsInsert = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否参与新增"),
                    IsUpdate = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否参与修改"),
                    IsSearch = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否参与搜索"),
                    SearchType = table.Column<int>(type: "int", nullable: false, comment: "搜索类型"),
                    IsShow = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否在表格中显示"),
                    CreationTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatorId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    LastModificationTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gen_table_column", x => x.Id);
                    table.ForeignKey(
                        name: "FK_gen_table_column_gen_table_GenTableId",
                        column: x => x.GenTableId,
                        principalTable: "gen_table",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "生成表的配置列")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sys_rolemenu",
                columns: table => new
                {
                    MenuId = table.Column<Guid>(type: "char(36)", nullable: false, comment: "菜单ID", collation: "ascii_general_ci"),
                    RoleId = table.Column<Guid>(type: "char(36)", nullable: false, comment: "角色ID", collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sys_rolemenu", x => new { x.RoleId, x.MenuId });
                    table.ForeignKey(
                        name: "FK_sys_rolemenu_sys_menu_MenuId",
                        column: x => x.MenuId,
                        principalTable: "sys_menu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_sys_rolemenu_sys_role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "sys_role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sys_userrole",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false, comment: "用户ID", collation: "ascii_general_ci"),
                    RoleId = table.Column<Guid>(type: "char(36)", nullable: false, comment: "角色ID", collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sys_userrole", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_sys_userrole_sys_role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "sys_role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_sys_userrole_sys_user_UserId",
                        column: x => x.UserId,
                        principalTable: "sys_user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "用户角色关联表")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_gen_table_TableName",
                table: "gen_table",
                column: "TableName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_gen_table_column_GenTableId",
                table: "gen_table_column",
                column: "GenTableId");

            migrationBuilder.CreateIndex(
                name: "IX_sys_config_Key",
                table: "sys_config",
                column: "Key",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_sys_dict_data_Key",
                table: "sys_dict_data",
                column: "Key",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_sys_dict_type_DictType",
                table: "sys_dict_type",
                column: "DictType",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_sys_rolemenu_MenuId",
                table: "sys_rolemenu",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_sys_userrole_RoleId",
                table: "sys_userrole",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "gen_table_column");

            migrationBuilder.DropTable(
                name: "org_dept");

            migrationBuilder.DropTable(
                name: "org_dept_employee");

            migrationBuilder.DropTable(
                name: "org_employee");

            migrationBuilder.DropTable(
                name: "org_position");

            migrationBuilder.DropTable(
                name: "org_positiongroup");

            migrationBuilder.DropTable(
                name: "sys_businesslog");

            migrationBuilder.DropTable(
                name: "sys_config");

            migrationBuilder.DropTable(
                name: "sys_dict_data");

            migrationBuilder.DropTable(
                name: "sys_dict_type");

            migrationBuilder.DropTable(
                name: "sys_loginlog");

            migrationBuilder.DropTable(
                name: "sys_rolemenu");

            migrationBuilder.DropTable(
                name: "sys_userrole");

            migrationBuilder.DropTable(
                name: "SysTenant");

            migrationBuilder.DropTable(
                name: "gen_table");

            migrationBuilder.DropTable(
                name: "sys_menu");

            migrationBuilder.DropTable(
                name: "sys_role");

            migrationBuilder.DropTable(
                name: "sys_user");
        }
    }
}
