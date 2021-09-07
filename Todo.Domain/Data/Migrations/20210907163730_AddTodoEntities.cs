using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Todo.Domain.Data.Migrations
{
    public partial class AddTodoEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TodoEntity",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: false),
                    Position = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Edited = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<DateTime>(nullable: true),
                    AspNetUserId = table.Column<string>(nullable: true),
                    AspNetUsersId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TodoEntity_AspNetUsers_AspNetUserId",
                        column: x => x.AspNetUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TodoCheckEntity",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TodoId = table.Column<int>(nullable: false),
                    Position = table.Column<int>(nullable: false),
                    TaskDescription = table.Column<string>(nullable: false),
                    Done = table.Column<bool>(nullable: false),
                    Edited = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoCheckEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TodoCheckEntity_TodoEntity_TodoId",
                        column: x => x.TodoId,
                        principalTable: "TodoEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TodoCheckEntity_TodoId",
                table: "TodoCheckEntity",
                column: "TodoId");

            migrationBuilder.CreateIndex(
                name: "IX_TodoCheckEntity_Id_TodoId",
                table: "TodoCheckEntity",
                columns: new[] { "Id", "TodoId" });

            migrationBuilder.CreateIndex(
                name: "IX_TodoEntity_AspNetUserId",
                table: "TodoEntity",
                column: "AspNetUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TodoEntity_Id_AspNetUsersId",
                table: "TodoEntity",
                columns: new[] { "Id", "AspNetUsersId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TodoCheckEntity");

            migrationBuilder.DropTable(
                name: "TodoEntity");
        }
    }
}
