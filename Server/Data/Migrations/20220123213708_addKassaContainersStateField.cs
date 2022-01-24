using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace kassablad.app.Server.Migrations
{
    public partial class addKassaContainersStateField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Concept",
                table: "KassaContainers");

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "KassaContainers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "KassaContainers");

            migrationBuilder.AddColumn<bool>(
                name: "Concept",
                table: "KassaContainers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
