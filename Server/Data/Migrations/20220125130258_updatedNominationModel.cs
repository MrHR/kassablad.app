using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace kassablad.app.Server.Migrations
{
    public partial class updatedNominationModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Total",
                table: "Nominations",
                newName: "Value");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Value",
                table: "Nominations",
                newName: "Total");
        }
    }
}
