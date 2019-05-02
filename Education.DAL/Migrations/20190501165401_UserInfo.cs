using Microsoft.EntityFrameworkCore.Migrations;

namespace Education.DAL.Migrations
{
    public partial class UserInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LectorId",
                table: "Lessons",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Patronimic",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_LectorId",
                table: "Lessons",
                column: "LectorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Lectors_LectorId",
                table: "Lessons",
                column: "LectorId",
                principalTable: "Lectors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Lectors_LectorId",
                table: "Lessons");

            migrationBuilder.DropIndex(
                name: "IX_Lessons_LectorId",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "LectorId",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Patronimic",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Surname",
                table: "AspNetUsers");
        }
    }
}
