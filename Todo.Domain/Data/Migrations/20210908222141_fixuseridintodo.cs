using Microsoft.EntityFrameworkCore.Migrations;

namespace Todo.Domain.Data.Migrations
{
    public partial class fixuseridintodo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TodoEntity_Id_AspNetUsersId",
                table: "TodoEntity");

            migrationBuilder.DropColumn(
                name: "AspNetUsersId",
                table: "TodoEntity");

            migrationBuilder.CreateIndex(
                name: "IX_TodoEntity_Id_AspNetUserId",
                table: "TodoEntity",
                columns: new[] { "Id", "AspNetUserId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TodoEntity_Id_AspNetUserId",
                table: "TodoEntity");

            migrationBuilder.AddColumn<string>(
                name: "AspNetUsersId",
                table: "TodoEntity",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TodoEntity_Id_AspNetUsersId",
                table: "TodoEntity",
                columns: new[] { "Id", "AspNetUsersId" });
        }
    }
}
