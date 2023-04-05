using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WildPrices.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PriceHistories_Products_ProductEntityId",
                table: "PriceHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_AspNetUsers_UserEntityId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_UserEntityId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_PriceHistories_ProductEntityId",
                table: "PriceHistories");

            migrationBuilder.DropColumn(
                name: "UserEntityId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductEntityId",
                table: "PriceHistories");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Products",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Products_UserId",
                table: "Products",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceHistories_ProductId",
                table: "PriceHistories",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_PriceHistories_Products_ProductId",
                table: "PriceHistories",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_AspNetUsers_UserId",
                table: "Products",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PriceHistories_Products_ProductId",
                table: "PriceHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_AspNetUsers_UserId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_UserId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_PriceHistories_ProductId",
                table: "PriceHistories");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "UserEntityId",
                table: "Products",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductEntityId",
                table: "PriceHistories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Products_UserEntityId",
                table: "Products",
                column: "UserEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceHistories_ProductEntityId",
                table: "PriceHistories",
                column: "ProductEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_PriceHistories_Products_ProductEntityId",
                table: "PriceHistories",
                column: "ProductEntityId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_AspNetUsers_UserEntityId",
                table: "Products",
                column: "UserEntityId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
