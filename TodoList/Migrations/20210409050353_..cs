using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TodoList.Migrations
{
    public partial class _ : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    username = table.Column<string>(type: "TEXT", nullable: false),
                    password = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.username);
                });

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    logged = table.Column<bool>(type: "INTEGER", nullable: false),
                    username = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lists",
                columns: table => new
                {
                    ListId = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    TimeCreate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DeleteHeight = table.Column<string>(type: "TEXT", nullable: true),
                    Accountusername = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lists", x => x.ListId);
                    table.ForeignKey(
                        name: "FK_Lists_Accounts_Accountusername",
                        column: x => x.Accountusername,
                        principalTable: "Accounts",
                        principalColumn: "username",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ToDoItem",
                columns: table => new
                {
                    ItemId = table.Column<string>(type: "TEXT", nullable: false),
                    ListId = table.Column<string>(type: "TEXT", nullable: true),
                    TimeCreate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TimeRemind = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Important = table.Column<bool>(type: "INTEGER", nullable: false),
                    Completed = table.Column<bool>(type: "INTEGER", nullable: false),
                    Content = table.Column<string>(type: "TEXT", nullable: true),
                    Title = table.Column<string>(type: "TEXT", nullable: true),
                    ContentHeight = table.Column<string>(type: "TEXT", nullable: true),
                    DeleteHeight = table.Column<string>(type: "TEXT", nullable: true),
                    ToDoListListId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToDoItem", x => x.ItemId);
                    table.ForeignKey(
                        name: "FK_ToDoItem_Lists_ToDoListListId",
                        column: x => x.ToDoListListId,
                        principalTable: "Lists",
                        principalColumn: "ListId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Username",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    username = table.Column<string>(type: "TEXT", nullable: true),
                    ListId = table.Column<string>(type: "TEXT", nullable: true),
                    ToDoListListId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Username", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Username_Lists_ToDoListListId",
                        column: x => x.ToDoListListId,
                        principalTable: "Lists",
                        principalColumn: "ListId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lists_Accountusername",
                table: "Lists",
                column: "Accountusername");

            migrationBuilder.CreateIndex(
                name: "IX_ToDoItem_ToDoListListId",
                table: "ToDoItem",
                column: "ToDoListListId");

            migrationBuilder.CreateIndex(
                name: "IX_Username_ToDoListListId",
                table: "Username",
                column: "ToDoListListId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DropTable(
                name: "ToDoItem");

            migrationBuilder.DropTable(
                name: "Username");

            migrationBuilder.DropTable(
                name: "Lists");

            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}
