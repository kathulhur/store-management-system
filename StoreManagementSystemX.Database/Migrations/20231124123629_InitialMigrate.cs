using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StoreManagementSystemX.Database.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigrate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedById = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Barcode = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    CostPrice = table.Column<decimal>(type: "TEXT", nullable: false),
                    SellingPrice = table.Column<decimal>(type: "TEXT", nullable: false),
                    InStock = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedById = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StockPurchases",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    DateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    MadeById = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockPurchases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockPurchases_Users_MadeById",
                        column: x => x.MadeById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    DateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    SellerId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Users_SellerId",
                        column: x => x.SellerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StockPurchaseProducts",
                columns: table => new
                {
                    StockPurchaseId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProductId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    QuantityBought = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockPurchaseProducts", x => new { x.ProductId, x.StockPurchaseId });
                    table.ForeignKey(
                        name: "FK_StockPurchaseProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockPurchaseProducts_StockPurchases_StockPurchaseId",
                        column: x => x.StockPurchaseId,
                        principalTable: "StockPurchases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PayLaters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    TransactionId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CustomerName = table.Column<string>(type: "TEXT", nullable: false),
                    IsPaid = table.Column<bool>(type: "INTEGER", nullable: false),
                    PaidAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayLaters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PayLaters_Transactions_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "Transactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransactionProducts",
                columns: table => new
                {
                    TransactionId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProductId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProductName = table.Column<string>(type: "TEXT", nullable: false),
                    QuantityBought = table.Column<int>(type: "INTEGER", nullable: false),
                    PriceSold = table.Column<decimal>(type: "TEXT", nullable: false),
                    CostPrice = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionProducts", x => new { x.ProductId, x.TransactionId });
                    table.ForeignKey(
                        name: "FK_TransactionProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransactionProducts_Transactions_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "Transactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedById", "Password", "Username" },
                values: new object[] { new Guid("04fc04ab-a071-4d9d-98f4-aa2568ed7700"), null, "password", "admin" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Barcode", "CostPrice", "CreatedById", "InStock", "Name", "SellingPrice" },
                values: new object[,]
                {
                    { new Guid("435e33dc-6d75-4121-87cb-216e43a97143"), "1", 11m, new Guid("04fc04ab-a071-4d9d-98f4-aa2568ed7700"), 2, "Product 1", 21m },
                    { new Guid("c5b4cff3-a38d-49e7-b47f-ffdffcf5f257"), "3", 13m, new Guid("04fc04ab-a071-4d9d-98f4-aa2568ed7700"), 5, "Product 3", 23m },
                    { new Guid("dec82413-2723-4ad8-a3c2-20da0cd06b72"), "2", 12m, new Guid("04fc04ab-a071-4d9d-98f4-aa2568ed7700"), 3, "Product 2", 22m }
                });

            migrationBuilder.InsertData(
                table: "StockPurchases",
                columns: new[] { "Id", "DateTime", "MadeById" },
                values: new object[] { new Guid("dfc02b33-bc9c-4fb6-8c2f-ccb477fd2090"), new DateTime(2023, 11, 24, 20, 36, 28, 746, DateTimeKind.Local).AddTicks(5667), new Guid("04fc04ab-a071-4d9d-98f4-aa2568ed7700") });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "Id", "DateTime", "SellerId" },
                values: new object[,]
                {
                    { new Guid("70bc0813-615a-427a-b562-516f7151f0cf"), new DateTime(2023, 11, 24, 20, 36, 28, 746, DateTimeKind.Local).AddTicks(5634), new Guid("04fc04ab-a071-4d9d-98f4-aa2568ed7700") },
                    { new Guid("f076a70f-3597-4c08-bc91-4f06aa92e3d7"), new DateTime(2023, 11, 24, 20, 36, 28, 746, DateTimeKind.Local).AddTicks(5655), new Guid("04fc04ab-a071-4d9d-98f4-aa2568ed7700") }
                });

            migrationBuilder.InsertData(
                table: "PayLaters",
                columns: new[] { "Id", "CustomerName", "IsPaid", "PaidAt", "TransactionId" },
                values: new object[] { new Guid("660a414f-d862-4384-9176-05b43ac82254"), "", false, new DateTime(2023, 11, 24, 20, 36, 28, 746, DateTimeKind.Local).AddTicks(5660), new Guid("f076a70f-3597-4c08-bc91-4f06aa92e3d7") });

            migrationBuilder.InsertData(
                table: "StockPurchaseProducts",
                columns: new[] { "ProductId", "StockPurchaseId", "Price", "QuantityBought" },
                values: new object[,]
                {
                    { new Guid("435e33dc-6d75-4121-87cb-216e43a97143"), new Guid("dfc02b33-bc9c-4fb6-8c2f-ccb477fd2090"), 11m, 2 },
                    { new Guid("c5b4cff3-a38d-49e7-b47f-ffdffcf5f257"), new Guid("dfc02b33-bc9c-4fb6-8c2f-ccb477fd2090"), 13m, 5 },
                    { new Guid("dec82413-2723-4ad8-a3c2-20da0cd06b72"), new Guid("dfc02b33-bc9c-4fb6-8c2f-ccb477fd2090"), 12m, 3 }
                });

            migrationBuilder.InsertData(
                table: "TransactionProducts",
                columns: new[] { "ProductId", "TransactionId", "CostPrice", "PriceSold", "ProductName", "QuantityBought" },
                values: new object[,]
                {
                    { new Guid("435e33dc-6d75-4121-87cb-216e43a97143"), new Guid("70bc0813-615a-427a-b562-516f7151f0cf"), 11m, 21m, "Product 1", 1 },
                    { new Guid("435e33dc-6d75-4121-87cb-216e43a97143"), new Guid("f076a70f-3597-4c08-bc91-4f06aa92e3d7"), 11m, 21m, "Product 1", 3 },
                    { new Guid("c5b4cff3-a38d-49e7-b47f-ffdffcf5f257"), new Guid("f076a70f-3597-4c08-bc91-4f06aa92e3d7"), 13m, 23m, "Product 3", 2 },
                    { new Guid("dec82413-2723-4ad8-a3c2-20da0cd06b72"), new Guid("70bc0813-615a-427a-b562-516f7151f0cf"), 12m, 22m, "Product 2", 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PayLaters_TransactionId",
                table: "PayLaters",
                column: "TransactionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_CreatedById",
                table: "Products",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_StockPurchaseProducts_StockPurchaseId",
                table: "StockPurchaseProducts",
                column: "StockPurchaseId");

            migrationBuilder.CreateIndex(
                name: "IX_StockPurchases_MadeById",
                table: "StockPurchases",
                column: "MadeById");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionProducts_TransactionId",
                table: "TransactionProducts",
                column: "TransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_SellerId",
                table: "Transactions",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CreatedById",
                table: "Users",
                column: "CreatedById");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PayLaters");

            migrationBuilder.DropTable(
                name: "StockPurchaseProducts");

            migrationBuilder.DropTable(
                name: "TransactionProducts");

            migrationBuilder.DropTable(
                name: "StockPurchases");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
