using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pharmacy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateDiscountTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrls",
                table: "products",
                newName: "ImageUrlsJson");

            migrationBuilder.RenameColumn(
                name: "ImagePublicIds",
                table: "products",
                newName: "ImagePublicIdsJson");

            migrationBuilder.RenameColumn(
                name: "Discount",
                table: "products",
                newName: "DiscountId");

            migrationBuilder.CreateTable(
                name: "discounts",
                columns: table => new
                {
                    DiscountId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDateUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDateUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Percentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_discounts", x => x.DiscountId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_products_DiscountId",
                table: "products",
                column: "DiscountId");

            migrationBuilder.AddForeignKey(
                name: "FK_products_discounts_DiscountId",
                table: "products",
                column: "DiscountId",
                principalTable: "discounts",
                principalColumn: "DiscountId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_discounts_DiscountId",
                table: "products");

            migrationBuilder.DropTable(
                name: "discounts");

            migrationBuilder.DropIndex(
                name: "IX_products_DiscountId",
                table: "products");

            migrationBuilder.RenameColumn(
                name: "ImageUrlsJson",
                table: "products",
                newName: "ImageUrls");

            migrationBuilder.RenameColumn(
                name: "ImagePublicIdsJson",
                table: "products",
                newName: "ImagePublicIds");

            migrationBuilder.RenameColumn(
                name: "DiscountId",
                table: "products",
                newName: "Discount");
        }
    }
}
