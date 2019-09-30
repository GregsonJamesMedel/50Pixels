using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace _50Pixels.Migrations
{
    public partial class adddateuploadedandviewspropertyinphotos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateUploaded",
                table: "Photos",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Views",
                table: "Photos",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateUploaded",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "Views",
                table: "Photos");
        }
    }
}
