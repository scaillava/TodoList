using Microsoft.EntityFrameworkCore.Migrations;

namespace Todo.Domain.Data.Migrations
{
    public partial class fixusertoken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AspNetUsersId",
                table: "TokenEntity");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AspNetUsersId",
                table: "TokenEntity",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
