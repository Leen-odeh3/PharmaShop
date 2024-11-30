using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pharmacy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class HandleImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "products",
                newName: "ImageUrls");

            migrationBuilder.RenameColumn(
                name: "ImagePublicId",
                table: "products",
                newName: "ImagePublicIds");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "categories",
                newName: "ImageUrls");

            migrationBuilder.RenameColumn(
                name: "ImagePublicId",
                table: "categories",
                newName: "ImagePublicIds");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrls",
                table: "products",
                newName: "ImageUrl");

            migrationBuilder.RenameColumn(
                name: "ImagePublicIds",
                table: "products",
                newName: "ImagePublicId");

            migrationBuilder.RenameColumn(
                name: "ImageUrls",
                table: "categories",
                newName: "ImageUrl");

            migrationBuilder.RenameColumn(
                name: "ImagePublicIds",
                table: "categories",
                newName: "ImagePublicId");
        }
    }
}
