using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace kassablad.app.Server.Migrations
{
    public partial class addComputedTotalColumnToKassaNoms : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Total",
                table: "KassaNominations",
                type: "decimal(18,2)",
                nullable: false,
                computedColumnSql: "[Nom_Multiplier] + [Amount]",
                stored: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Total",
                table: "KassaNominations");
        }
    }
}
