using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WildPrices.Data.Migrations
{
    /// <inheritdoc />
    public partial class EditedMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Article",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Article",
                table: "Products");
        }
    }
}
