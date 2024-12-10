using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pharmacy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixIssuesDiscountEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_discounts_DiscountId",
                table: "products");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "discounts");

            migrationBuilder.AlterColumn<int>(
                name: "DiscountId",
                table: "products",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_products_discounts_DiscountId",
                table: "products",
                column: "DiscountId",
                principalTable: "discounts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_discounts_DiscountId",
                table: "products");

            migrationBuilder.AlterColumn<int>(
                name: "DiscountId",
                table: "products",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "discounts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_products_discounts_DiscountId",
                table: "products",
                column: "DiscountId",
                principalTable: "discounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
