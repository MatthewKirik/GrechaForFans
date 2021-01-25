using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Shops",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shops", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lots",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    ImageLink = table.Column<string>(type: "TEXT", nullable: false),
                    Link = table.Column<string>(type: "TEXT", nullable: false),
                    Manufacturer = table.Column<string>(type: "TEXT", nullable: true),
                    WeightInGrams = table.Column<int>(type: "INTEGER", nullable: false),
                    ShopId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lots_Shops_ShopId",
                        column: x => x.ShopId,
                        principalTable: "Shops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Prices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Value = table.Column<double>(type: "REAL", nullable: false),
                    LotId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prices_Lots_LotId",
                        column: x => x.LotId,
                        principalTable: "Lots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Shops",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Prom" });

            migrationBuilder.InsertData(
                table: "Shops",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Rozetka" });

            migrationBuilder.InsertData(
                table: "Shops",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "Epicentr" });

            migrationBuilder.CreateIndex(
                name: "IX_Lots_ShopId",
                table: "Lots",
                column: "ShopId");

            migrationBuilder.CreateIndex(
                name: "IX_Prices_LotId",
                table: "Prices",
                column: "LotId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Prices");

            migrationBuilder.DropTable(
                name: "Lots");

            migrationBuilder.DropTable(
                name: "Shops");
        }
    }
}
