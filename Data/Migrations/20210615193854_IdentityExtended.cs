using Microsoft.EntityFrameworkCore.Migrations;

namespace MyToDoProject.Data.Migrations
{
    public partial class IdentityExtended : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CetUserId",
                table: "Todos",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Todos_CetUserId",
                table: "Todos",
                column: "CetUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Todos_AspNetUsers_CetUserId",
                table: "Todos",
                column: "CetUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Todos_AspNetUsers_CetUserId",
                table: "Todos");

            migrationBuilder.DropIndex(
                name: "IX_Todos_CetUserId",
                table: "Todos");

            migrationBuilder.DropColumn(
                name: "CetUserId",
                table: "Todos");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AspNetUsers");
        }
    }
}
