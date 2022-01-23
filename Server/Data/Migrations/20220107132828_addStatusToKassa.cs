using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace kassablad.app.Server.Migrations
{
    public partial class addStatusToKassa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Kassas",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Kassas");
        }
    }
}
