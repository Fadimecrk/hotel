using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelApp.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kunder",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Namn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Epost = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kunder", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rum",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rumsnummer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Typ = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrisPerNatt = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rum", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bokningar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KundId = table.Column<int>(type: "int", nullable: false),
                    RumId = table.Column<int>(type: "int", nullable: false),
                    Incheckning = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Utcheckning = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bokningar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bokningar_Kunder_KundId",
                        column: x => x.KundId,
                        principalTable: "Kunder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bokningar_Rum_RumId",
                        column: x => x.RumId,
                        principalTable: "Rum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bokningar_KundId",
                table: "Bokningar",
                column: "KundId");

            migrationBuilder.CreateIndex(
                name: "IX_Bokningar_RumId",
                table: "Bokningar",
                column: "RumId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bokningar");

            migrationBuilder.DropTable(
                name: "Kunder");

            migrationBuilder.DropTable(
                name: "Rum");
        }
    }
}
