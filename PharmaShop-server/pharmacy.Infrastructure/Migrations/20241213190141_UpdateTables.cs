using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pharmacy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "paymentMethods",
                keyColumn: "PaymentMethodId",
                keyValue: new Guid("0f9e47c6-bd20-4a08-a066-55324b2e2076"));

            migrationBuilder.InsertData(
                table: "paymentMethods",
                columns: new[] { "PaymentMethodId", "PaymentMethodName" },
                values: new object[] { new Guid("26c40083-b3a4-42fc-902c-2fb58ab7d6a8"), "Credit Card" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "paymentMethods",
                keyColumn: "PaymentMethodId",
                keyValue: new Guid("26c40083-b3a4-42fc-902c-2fb58ab7d6a8"));

            migrationBuilder.InsertData(
                table: "paymentMethods",
                columns: new[] { "PaymentMethodId", "PaymentMethodName" },
                values: new object[] { new Guid("0f9e47c6-bd20-4a08-a066-55324b2e2076"), "Credit Card" });
        }
    }
}
