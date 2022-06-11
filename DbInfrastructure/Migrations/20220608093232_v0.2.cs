using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DbInfrastructure.Migrations
{
    public partial class v02 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropColumn(
                name: "Salt",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "location",
                table: "Product",
                newName: "Location");

            migrationBuilder.AddColumn<bool>(
                name: "IsLogged",
                table: "User",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLogged",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "Location",
                table: "Product",
                newName: "location");

            migrationBuilder.AddColumn<byte[]>(
                name: "Salt",
                table: "User",
                type: "BLOB",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    City = table.Column<string>(type: "TEXT", maxLength: 32, nullable: false),
                    Country = table.Column<string>(type: "TEXT", nullable: false),
                    HomeNumber = table.Column<string>(type: "TEXT", maxLength: 12, nullable: false),
                    PostalCode = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    Street = table.Column<string>(type: "TEXT", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Address_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Address_UserId",
                table: "Address",
                column: "UserId");
        }
    }
}
