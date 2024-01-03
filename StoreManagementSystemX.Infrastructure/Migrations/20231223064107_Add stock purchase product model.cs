using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoreManagementSystemX.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Addstockpurchaseproductmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("cf4a8a7a-37cd-408b-817f-d54570050ce3"));

            migrationBuilder.CreateTable(
                name: "StockPurchaseDBModel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    StockManagerId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockPurchaseDBModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockPurchaseDBModel_Users_StockManagerId",
                        column: x => x.StockManagerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StockPurchaseProductDBModel",
                columns: table => new
                {
                    StockPurchaseId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProductId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Barcode = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    QuantityBought = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockPurchaseProductDBModel", x => new { x.ProductId, x.StockPurchaseId });
                    table.ForeignKey(
                        name: "FK_StockPurchaseProductDBModel_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockPurchaseProductDBModel_StockPurchaseDBModel_StockPurchaseId",
                        column: x => x.StockPurchaseId,
                        principalTable: "StockPurchaseDBModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatorId", "Password", "Username" },
                values: new object[] { new Guid("6f765d6c-93a3-4666-b862-cdde776c0814"), null, "password", "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_StockPurchaseDBModel_StockManagerId",
                table: "StockPurchaseDBModel",
                column: "StockManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_StockPurchaseProductDBModel_StockPurchaseId",
                table: "StockPurchaseProductDBModel",
                column: "StockPurchaseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockPurchaseProductDBModel");

            migrationBuilder.DropTable(
                name: "StockPurchaseDBModel");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("6f765d6c-93a3-4666-b862-cdde776c0814"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatorId", "Password", "Username" },
                values: new object[] { new Guid("cf4a8a7a-37cd-408b-817f-d54570050ce3"), null, "password", "admin" });
        }
    }
}
