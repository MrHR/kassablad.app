using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace kassablad.app.Server.Migrations
{
    public partial class addRelationKassaContainerToApplicationUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FormSection",
                table: "KassaContainers");

            migrationBuilder.DropColumn(
                name: "NaamTapper",
                table: "KassaContainers");

            migrationBuilder.DropColumn(
                name: "NaamTapperSluit",
                table: "KassaContainers");

            migrationBuilder.CreateTable(
                name: "KassaContainerApplicationUsers",
                columns: table => new
                {
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    KassaContainerId = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KassaContainerApplicationUsers", x => new { x.ApplicationUserId, x.KassaContainerId });
                    table.ForeignKey(
                        name: "FK_KassaContainerApplicationUsers_ApplicationUsers_Id",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KassaContainerApplicationUsers_KassaContainers_KassaContainerId",
                        column: x => x.KassaContainerId,
                        principalTable: "KassaContainers",
                        principalColumn: "KassaContainerId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_KassaContainerApplicationUsers_KassaContainerId",
                table: "KassaContainerApplicationUsers",
                column: "KassaContainerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KassaContainerApplicationUsers");

            migrationBuilder.AddColumn<string>(
                name: "FormSection",
                table: "KassaContainers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NaamTapper",
                table: "KassaContainers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NaamTapperSluit",
                table: "KassaContainers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
