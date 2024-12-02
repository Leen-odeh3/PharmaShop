using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pharmacy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCategoryImageFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrls",
                table: "categories",
                newName: "ImageUrlsJson");

            migrationBuilder.RenameColumn(
                name: "ImagePublicIds",
                table: "categories",
                newName: "ImagePublicIdsJson");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrlsJson",
                table: "categories",
                newName: "ImageUrls");

            migrationBuilder.RenameColumn(
                name: "ImagePublicIdsJson",
                table: "categories",
                newName: "ImagePublicIds");
        }
    }
}
