using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cracker.Admin.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFiles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "deleter_id",
                table: "sys_file");

            migrationBuilder.DropColumn(
                name: "deletion_time",
                table: "sys_file");

            migrationBuilder.DropColumn(
                name: "is_deleted",
                table: "sys_file");

            migrationBuilder.DropColumn(
                name: "last_modification_time",
                table: "sys_file");

            migrationBuilder.DropColumn(
                name: "last_modifier_id",
                table: "sys_file");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "deleter_id",
                table: "sys_file",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "deletion_time",
                table: "sys_file",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                table: "sys_file",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "last_modification_time",
                table: "sys_file",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "last_modifier_id",
                table: "sys_file",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");
        }
    }
}
