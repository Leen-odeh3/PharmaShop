using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pharmacy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DeleteRowFromDb1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "paymentMethods",
                keyColumn: "PaymentMethodId",
                keyValue: new Guid("2a15d3b2-24a2-4aee-932f-a4ecd079c4e6"));

            migrationBuilder.InsertData(
                table: "paymentMethods",
                columns: new[] { "PaymentMethodId", "PaymentMethodName" },
                values: new object[] { new Guid("8dfa3264-c3ef-4c9a-a51c-7a2409b0fbf0"), "card" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "paymentMethods",
                keyColumn: "PaymentMethodId",
                keyValue: new Guid("8dfa3264-c3ef-4c9a-a51c-7a2409b0fbf0"));

            migrationBuilder.InsertData(
                table: "paymentMethods",
                columns: new[] { "PaymentMethodId", "PaymentMethodName" },
                values: new object[] { new Guid("2a15d3b2-24a2-4aee-932f-a4ecd079c4e6"), "Credit Card" });
        }
    }
}
