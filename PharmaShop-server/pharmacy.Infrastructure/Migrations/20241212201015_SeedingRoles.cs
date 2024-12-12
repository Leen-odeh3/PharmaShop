using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace pharmacy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedingRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "paymentMethods",
                keyColumn: "PaymentMethodId",
                keyValue: new Guid("4992c08e-4f6c-4261-8c8c-1815871bc5fc"));

            migrationBuilder.DeleteData(
                table: "paymentMethods",
                keyColumn: "PaymentMethodId",
                keyValue: new Guid("ea7ad791-c47c-4839-aeb1-8b6f00e230e5"));

            migrationBuilder.InsertData(
                table: "paymentMethods",
                columns: new[] { "PaymentMethodId", "PaymentMethodName" },
                values: new object[,]
                {
                    { new Guid("28061c68-4f5e-4b69-9d86-32862afa7120"), "Credit Card" },
                    { new Guid("bc019009-67fd-4522-a56d-6c04f0d2aa45"), "PayPal" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "paymentMethods",
                keyColumn: "PaymentMethodId",
                keyValue: new Guid("28061c68-4f5e-4b69-9d86-32862afa7120"));

            migrationBuilder.DeleteData(
                table: "paymentMethods",
                keyColumn: "PaymentMethodId",
                keyValue: new Guid("bc019009-67fd-4522-a56d-6c04f0d2aa45"));

            migrationBuilder.InsertData(
                table: "paymentMethods",
                columns: new[] { "PaymentMethodId", "PaymentMethodName" },
                values: new object[,]
                {
                    { new Guid("4992c08e-4f6c-4261-8c8c-1815871bc5fc"), "PayPal" },
                    { new Guid("ea7ad791-c47c-4839-aeb1-8b6f00e230e5"), "Credit Card" }
                });
        }
    }
}
