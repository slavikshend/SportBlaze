using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webapi.Migrations
{
    /// <inheritdoc />
    public partial class OrderDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_order_details_products_ProductId",
                table: "order_details");

            migrationBuilder.DropColumn(
                name: "user_email",
                table: "order_details");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "order_details",
                newName: "product_id");

            migrationBuilder.RenameIndex(
                name: "IX_order_details_ProductId",
                table: "order_details",
                newName: "IX_order_details_product_id");

            migrationBuilder.AddForeignKey(
                name: "FK_order_details_products_product_id",
                table: "order_details",
                column: "product_id",
                principalTable: "products",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_order_details_products_product_id",
                table: "order_details");

            migrationBuilder.RenameColumn(
                name: "product_id",
                table: "order_details",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_order_details_product_id",
                table: "order_details",
                newName: "IX_order_details_ProductId");

            migrationBuilder.AddColumn<string>(
                name: "user_email",
                table: "order_details",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_order_details_products_ProductId",
                table: "order_details",
                column: "ProductId",
                principalTable: "products",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
