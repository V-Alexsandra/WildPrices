using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WildPrices.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixProductEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDesiredPrice",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDesiredPrice",
                table: "Products");
        }
    }
}
