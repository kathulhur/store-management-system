using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoreManagementSystemX.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Makepaylateroptional : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("45289878-b6fa-4af7-8fba-3258d2f354ac"));

            migrationBuilder.AlterColumn<bool>(
                name: "PayLater_IsPaid",
                table: "Transactions",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<string>(
                name: "PayLater_CustomerName",
                table: "Transactions",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatorId", "Password", "Username" },
                values: new object[] { new Guid("cf4a8a7a-37cd-408b-817f-d54570050ce3"), null, "password", "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("cf4a8a7a-37cd-408b-817f-d54570050ce3"));

            migrationBuilder.AlterColumn<bool>(
                name: "PayLater_IsPaid",
                table: "Transactions",
                type: "INTEGER",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PayLater_CustomerName",
                table: "Transactions",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatorId", "Password", "Username" },
                values: new object[] { new Guid("45289878-b6fa-4af7-8fba-3258d2f354ac"), null, "password", "admin" });
        }
    }
}
