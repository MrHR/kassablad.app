using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace kassablad.app.Server.Migrations
{
    public partial class updatedNomModelValueToText : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Value",
                table: "Nominations",
                newName: "Text");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Text",
                table: "Nominations",
                newName: "Value");
        }
    }
}
