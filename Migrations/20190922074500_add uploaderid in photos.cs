using Microsoft.EntityFrameworkCore.Migrations;

namespace _50Pixels.Migrations
{
    public partial class adduploaderidinphotos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UploaderId",
                table: "Photos",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Photos_UploaderId",
                table: "Photos",
                column: "UploaderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_AspNetUsers_UploaderId",
                table: "Photos",
                column: "UploaderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_AspNetUsers_UploaderId",
                table: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_Photos_UploaderId",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "UploaderId",
                table: "Photos");
        }
    }
}
