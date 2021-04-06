using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TodoList.Migrations
{
    public partial class _ : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeDispose",
                table: "ToDoItem");

            migrationBuilder.AddColumn<bool>(
                name: "Completed",
                table: "ToDoItem",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Important",
                table: "ToDoItem",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "ToDoItem",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TimeCreate",
                table: "Lists",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DropColumn(
                name: "Completed",
                table: "ToDoItem");

            migrationBuilder.DropColumn(
                name: "Important",
                table: "ToDoItem");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "ToDoItem");

            migrationBuilder.DropColumn(
                name: "TimeCreate",
                table: "Lists");

            migrationBuilder.AddColumn<DateTime>(
                name: "TimeDispose",
                table: "ToDoItem",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
