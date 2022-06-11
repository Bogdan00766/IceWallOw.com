using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DbInfrastructure.Migrations
{
    public partial class v03 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLogged",
                table: "User");

            migrationBuilder.AddColumn<string>(
                name: "AutoLoginGUID",
                table: "User",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "AutoLoginGUIDExpires",
                table: "User",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AutoLoginGUID",
                table: "User");

            migrationBuilder.DropColumn(
                name: "AutoLoginGUIDExpires",
                table: "User");

            migrationBuilder.AddColumn<bool>(
                name: "IsLogged",
                table: "User",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }
    }
}
