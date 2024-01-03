using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoreManagementSystemX.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Removetotalamounginstockpurchasetable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("6f765d6c-93a3-4666-b862-cdde776c0814"));

            migrationBuilder.DropColumn(
                name: "TotalAmount",
                table: "StockPurchaseDBModel");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatorId", "Password", "Username" },
                values: new object[] { new Guid("6b05b739-d2fc-4616-ad3a-1e476c690ff5"), null, "password", "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("6b05b739-d2fc-4616-ad3a-1e476c690ff5"));

            migrationBuilder.AddColumn<decimal>(
                name: "TotalAmount",
                table: "StockPurchaseDBModel",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatorId", "Password", "Username" },
                values: new object[] { new Guid("6f765d6c-93a3-4666-b862-cdde776c0814"), null, "password", "admin" });
        }
    }
}
