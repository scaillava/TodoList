using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Todo.Domain.Data.Migrations
{
    public partial class todotask : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TodoCheckEntity");

            migrationBuilder.CreateTable(
                name: "TodoTaskEntity",
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
                    table.PrimaryKey("PK_TodoTaskEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TodoTaskEntity_TodoEntity_TodoId",
                        column: x => x.TodoId,
                        principalTable: "TodoEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TodoTaskEntity_TodoId",
                table: "TodoTaskEntity",
                column: "TodoId");

            migrationBuilder.CreateIndex(
                name: "IX_TodoTaskEntity_Id_TodoId",
                table: "TodoTaskEntity",
                columns: new[] { "Id", "TodoId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TodoTaskEntity");

            migrationBuilder.CreateTable(
                name: "TodoCheckEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Done = table.Column<bool>(type: "bit", nullable: false),
                    Edited = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false),
                    TaskDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TodoId = table.Column<int>(type: "int", nullable: false)
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
        }
    }
}
