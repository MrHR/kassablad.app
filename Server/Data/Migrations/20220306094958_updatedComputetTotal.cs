using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace kassablad.app.Server.Migrations
{
    public partial class updatedComputetTotal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Total",
                table: "KassaNominations",
                type: "decimal(18,2)",
                nullable: false,
                computedColumnSql: "[Nom_Multiplier] * [Amount]",
                stored: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComputedColumnSql: "[Nom_Multiplier] + [Amount]",
                oldStored: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Total",
                table: "KassaNominations",
                type: "decimal(18,2)",
                nullable: false,
                computedColumnSql: "[Nom_Multiplier] + [Amount]",
                stored: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComputedColumnSql: "[Nom_Multiplier] * [Amount]",
                oldStored: true);
        }
    }
}
