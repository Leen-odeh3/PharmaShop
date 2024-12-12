using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace pharmacy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTypePK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "paymentMethods",
                columns: new[] { "PaymentMethodId", "PaymentMethodName" },
                values: new object[,]
                {
                    { new Guid("3b66e66b-4866-4dcd-9c7a-7eace5de7b6b"), "Credit Card" },
                    { new Guid("9455d543-8e39-415e-bee8-ea46d0cbb868"), "PayPal" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
           
        }
    }
}
