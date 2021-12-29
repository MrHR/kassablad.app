using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace kassablad.app.Server.Migrations
{
    public partial class addNomOwnedTypeToKassaNominations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "KassaNominations");

            migrationBuilder.AddColumn<string>(
                name: "Nom_Currency",
                table: "KassaNominations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Nom_Multiplier",
                table: "KassaNominations",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nom_Currency",
                table: "KassaNominations");

            migrationBuilder.DropColumn(
                name: "Nom_Multiplier",
                table: "KassaNominations");

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "KassaNominations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
