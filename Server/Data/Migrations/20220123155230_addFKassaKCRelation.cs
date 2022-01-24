using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace kassablad.app.Server.Migrations
{
    public partial class addFKassaKCRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FKassaId",
                table: "KassaContainers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "FKassa",
                columns: table => new
                {
                    FKassaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FKassaNaam = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FKassa", x => x.FKassaId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KassaContainers_FKassaId",
                table: "KassaContainers",
                column: "FKassaId");

            migrationBuilder.AddForeignKey(
                name: "FK_KassaContainers_FKassa_FKassaId",
                table: "KassaContainers",
                column: "FKassaId",
                principalTable: "FKassa",
                principalColumn: "FKassaId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KassaContainers_FKassa_FKassaId",
                table: "KassaContainers");

            migrationBuilder.DropTable(
                name: "FKassa");

            migrationBuilder.DropIndex(
                name: "IX_KassaContainers_FKassaId",
                table: "KassaContainers");

            migrationBuilder.DropColumn(
                name: "FKassaId",
                table: "KassaContainers");
        }
    }
}
