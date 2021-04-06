using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TodoList.Migrations
{
    public partial class InitialCreate : Migration
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
                name: "Lists",
                columns: table => new
                {
                    ListId = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lists", x => x.ListId);
                });

            migrationBuilder.CreateTable(
                name: "AccountToDoList",
                columns: table => new
                {
                    Ownersusername = table.Column<string>(type: "TEXT", nullable: false),
                    listsListId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountToDoList", x => new { x.Ownersusername, x.listsListId });
                    table.ForeignKey(
                        name: "FK_AccountToDoList_Accounts_Ownersusername",
                        column: x => x.Ownersusername,
                        principalTable: "Accounts",
                        principalColumn: "username",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountToDoList_Lists_listsListId",
                        column: x => x.listsListId,
                        principalTable: "Lists",
                        principalColumn: "ListId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ToDoItem",
                columns: table => new
                {
                    ItemId = table.Column<string>(type: "TEXT", nullable: false),
                    ListId = table.Column<string>(type: "TEXT", nullable: true),
                    TimeCreate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TimeDispose = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Content = table.Column<string>(type: "TEXT", nullable: true),
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

            migrationBuilder.CreateIndex(
                name: "IX_AccountToDoList_listsListId",
                table: "AccountToDoList",
                column: "listsListId");

            migrationBuilder.CreateIndex(
                name: "IX_ToDoItem_ToDoListListId",
                table: "ToDoItem",
                column: "ToDoListListId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountToDoList");

            migrationBuilder.DropTable(
                name: "ToDoItem");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Lists");
        }
    }
}
