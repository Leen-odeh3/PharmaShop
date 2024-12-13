using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pharmacy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DeleteRowFromDb2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "paymentMethods",
                keyColumn: "PaymentMethodId",
                keyValue: new Guid("8dfa3264-c3ef-4c9a-a51c-7a2409b0fbf0"));

            migrationBuilder.InsertData(
                table: "paymentMethods",
                columns: new[] { "PaymentMethodId", "PaymentMethodName" },
                values: new object[] { new Guid("0f9e47c6-bd20-4a08-a066-55324b2e2076"), "Credit Card" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "paymentMethods",
                keyColumn: "PaymentMethodId",
                keyValue: new Guid("0f9e47c6-bd20-4a08-a066-55324b2e2076"));

            migrationBuilder.InsertData(
                table: "paymentMethods",
                columns: new[] { "PaymentMethodId", "PaymentMethodName" },
                values: new object[] { new Guid("8dfa3264-c3ef-4c9a-a51c-7a2409b0fbf0"), "card" });
        }
    }
}
