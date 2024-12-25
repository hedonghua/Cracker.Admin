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
                name: "org_dept",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    code = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false, comment: "部门编号")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    name = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false, comment: "部门名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    sort = table.Column<int>(type: "int", nullable: false, comment: "排序"),
                    description = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: true, comment: "描述")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    status = table.Column<int>(type: "int", nullable: false, comment: "状态：1正常2停用"),
                    curatorId = table.Column<Guid>(type: "char(36)", nullable: true, comment: "负责人", collation: "ascii_general_ci"),
                    email = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true, comment: "邮箱")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    phone = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true, comment: "电话")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    parent_id = table.Column<Guid>(type: "char(36)", nullable: true, comment: "父ID", collation: "ascii_general_ci"),
                    parent_ids = table.Column<string>(type: "varchar(1024)", maxLength: 1024, nullable: true, comment: "层级IDS")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    layer = table.Column<int>(type: "int", nullable: false, comment: "层级"),
                    extra_properties = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    concurrency_stamp = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    creation_time = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    creator_id = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    last_modification_time = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    last_modifier_id = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    is_deleted = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    deleter_id = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    deletion_time = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("id", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "org_dept_employee",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    employee_id = table.Column<Guid>(type: "char(36)", nullable: false, comment: "员工ID", collation: "ascii_general_ci"),
                    dept_id = table.Column<Guid>(type: "char(36)", nullable: false, comment: "部门ID", collation: "ascii_general_ci"),
                    position_id = table.Column<Guid>(type: "char(36)", nullable: false, comment: "职位ID", collation: "ascii_general_ci"),
                    is_main = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否主职位"),
                    extra_properties = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    concurrency_stamp = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    creation_time = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    creator_id = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    last_modification_time = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    last_modifier_id = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    is_deleted = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    deleter_id = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    deletion_time = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("id", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "org_employee",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    code = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false, comment: "工号")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    name = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false, comment: "姓名")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Sex = table.Column<int>(type: "int", nullable: false, comment: "性别"),
                    phone = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: false, comment: "手机号码")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    idno = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false, comment: "身份证")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    front_idno_url = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: true, comment: "身份证正面")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    back_idno_url = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: true, comment: "身份证背面")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    birthday = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "生日"),
                    address = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: true, comment: "现住址")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true, comment: "邮箱")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    in_time = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "入职时间"),
                    out_time = table.Column<DateTime>(type: "datetime(6)", nullable: true, comment: "离职时间"),
                    is_out = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否离职"),
                    user_id = table.Column<Guid>(type: "char(36)", nullable: true, comment: "关联用户ID", collation: "ascii_general_ci"),
                    dept_id = table.Column<Guid>(type: "char(36)", nullable: false, comment: "部门ID", collation: "ascii_general_ci"),
                    position_id = table.Column<Guid>(type: "char(36)", nullable: false, comment: "职位ID", collation: "ascii_general_ci"),
                    extra_properties = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    concurrency_stamp = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    creation_time = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    creator_id = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    last_modification_time = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    last_modifier_id = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    is_deleted = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    deleter_id = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    deletion_time = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("id", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "org_position",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    code = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false, comment: "职位编号")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    name = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false, comment: "职位名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    level = table.Column<int>(type: "int", nullable: false, comment: "职级"),
                    status = table.Column<int>(type: "int", nullable: false, comment: "状态：1正常2停用"),
                    description = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: true, comment: "描述")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    group_id = table.Column<Guid>(type: "char(36)", nullable: true, comment: "职位分组ID", collation: "ascii_general_ci"),
                    extra_properties = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    concurrency_stamp = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    creation_time = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    creator_id = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    last_modification_time = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    last_modifier_id = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    is_deleted = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    deleter_id = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    deletion_time = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("id", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "org_position_group",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    group_name = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false, comment: "职位名")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    remark = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: true, comment: "备注")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    parent_id = table.Column<Guid>(type: "char(36)", nullable: true, comment: "父级ID", collation: "ascii_general_ci"),
                    parent_ids = table.Column<string>(type: "varchar(1024)", maxLength: 1024, nullable: true, comment: "层级父ID")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    creation_time = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    creator_id = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    last_modification_time = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    last_modifier_id = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("id", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sys_business_log",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    user_name = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false, comment: "账号")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    action = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: false, comment: "操作方法，全名")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    http_method = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: false, comment: "http请求方式")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    url = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: false, comment: "请求地址")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    os = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true, comment: "系统")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    browser = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true, comment: "浏览器")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    node_name = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false, comment: "操作节点名")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ip = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true, comment: "IP")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    address = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, comment: "登录地址")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_success = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否成功"),
                    operation_msg = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true, comment: "操作信息")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    mill_seconds = table.Column<int>(type: "int", nullable: false, comment: "耗时，单位毫秒"),
                    creation_time = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    creator_id = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    last_modification_time = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    last_modifier_id = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("id", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sys_dict",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    key = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false, comment: "字典键")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    value = table.Column<string>(type: "varchar(1024)", maxLength: 1024, nullable: false, comment: "字典值")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    label = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true, comment: "显示文本")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    group_name = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true, comment: "组名")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    remark = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: true, comment: "备注")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    sort = table.Column<int>(type: "int", nullable: false, comment: "排序值"),
                    is_enabled = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否开启"),
                    creation_time = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    creator_id = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    last_modification_time = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    last_modifier_id = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    is_deleted = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    deleter_id = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    deletion_time = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("id", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sys_file",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    unique_name = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false, comment: "文件编号/BLOB名")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    file_name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false, comment: "文件名")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    size = table.Column<int>(type: "int", nullable: false, comment: "单位KB"),
                    suffix = table.Column<string>(type: "longtext", nullable: true, comment: "后缀")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    relative_url = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: true, comment: "后缀")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    mime_type = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true, comment: "媒体类型")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    extra_path = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: true, comment: "额外地址")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    upload_type = table.Column<int>(type: "int", nullable: false, comment: "上传类型 9:BLOB文件系统"),
                    creation_time = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    creator_id = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    last_modification_time = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    last_modifier_id = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    is_deleted = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    deleter_id = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    deletion_time = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("id", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sys_login_log",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    user_name = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false, comment: "账号")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ip = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true, comment: "IP")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    address = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, comment: "登录地址")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    os = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true, comment: "系统")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    browser = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true, comment: "浏览器")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    operation_msg = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true, comment: "操作信息")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_success = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否成功"),
                    creation_time = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    creator_id = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    last_modification_time = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    last_modifier_id = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("id", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sys_menu",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    title = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false, comment: "显示标题/名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    name = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true, comment: "组件名")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    icon = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true, comment: "图标")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    path = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, comment: "路由/地址")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    function_type = table.Column<int>(type: "int", nullable: false, comment: "功能类型"),
                    permission = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true, comment: "授权码")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    parent_id = table.Column<Guid>(type: "char(36)", nullable: true, comment: "父级ID", collation: "ascii_general_ci"),
                    sort = table.Column<int>(type: "int", nullable: false, comment: "排序"),
                    hidden = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否隐藏"),
                    extra_properties = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    concurrency_stamp = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    creation_time = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    creator_id = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    last_modification_time = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    last_modifier_id = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    is_deleted = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    deleter_id = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    deletion_time = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("id", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sys_role",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    role_name = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false, comment: "角色名")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    remark = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: true, comment: "备注")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    extra_properties = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    concurrency_stamp = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    creation_time = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    creator_id = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    last_modification_time = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    last_modifier_id = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    is_deleted = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    deleter_id = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    deletion_time = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("id", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sys_role_menu",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    menu_id = table.Column<Guid>(type: "char(36)", nullable: false, comment: "菜单ID", collation: "ascii_general_ci"),
                    role_id = table.Column<Guid>(type: "char(36)", nullable: false, comment: "角色ID", collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("id", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sys_user",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    username = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false, comment: "用户名")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    password = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: false, comment: "密码")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    password_salt = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false, comment: "密码盐")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    avatar = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, comment: "头像")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    nickname = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false, comment: "昵称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    sex = table.Column<int>(type: "int", nullable: false, comment: "性别"),
                    is_enabled = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否启用"),
                    extra_properties = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    concurrency_stamp = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    creation_time = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    creator_id = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    last_modification_time = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    last_modifier_id = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    is_deleted = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    deleter_id = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    deletion_time = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("id", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sys_user_role",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    user_id = table.Column<Guid>(type: "char(36)", nullable: false, comment: "用户ID", collation: "ascii_general_ci"),
                    role_id = table.Column<Guid>(type: "char(36)", nullable: false, comment: "角色ID", collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("id", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "org_dept");

            migrationBuilder.DropTable(
                name: "org_dept_employee");

            migrationBuilder.DropTable(
                name: "org_employee");

            migrationBuilder.DropTable(
                name: "org_position");

            migrationBuilder.DropTable(
                name: "org_position_group");

            migrationBuilder.DropTable(
                name: "sys_business_log");

            migrationBuilder.DropTable(
                name: "sys_dict");

            migrationBuilder.DropTable(
                name: "sys_file");

            migrationBuilder.DropTable(
                name: "sys_login_log");

            migrationBuilder.DropTable(
                name: "sys_menu");

            migrationBuilder.DropTable(
                name: "sys_role");

            migrationBuilder.DropTable(
                name: "sys_role_menu");

            migrationBuilder.DropTable(
                name: "sys_user");

            migrationBuilder.DropTable(
                name: "sys_user_role");
        }
    }
}
