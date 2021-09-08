using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Todo.Domain.Data.Migrations
{
    public partial class addusertokens : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TokenEntity",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<Guid>(nullable: false),
                    AspNetUserId = table.Column<string>(nullable: true),
                    AspNetUsersId = table.Column<string>(nullable: true),
                    Expiration = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TokenEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TokenEntity_AspNetUsers_AspNetUserId",
                        column: x => x.AspNetUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TokenEntity_AspNetUserId",
                table: "TokenEntity",
                column: "AspNetUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TokenEntity");
        }
    }
}
