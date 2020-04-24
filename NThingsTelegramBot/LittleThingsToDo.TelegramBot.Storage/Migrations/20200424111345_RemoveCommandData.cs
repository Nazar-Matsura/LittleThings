using Microsoft.EntityFrameworkCore.Migrations;

namespace LittleThingsToDo.TelegramBot.Storage.Migrations
{
    public partial class RemoveCommandData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Data",
                table: "LastCommands");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Data",
                table: "LastCommands",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
