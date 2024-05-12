using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webapi.Migrations
{
    /// <inheritdoc />
    public partial class wregwwe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orders_payments_payment_id",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "IX_payments_order_id",
                table: "payments");

            migrationBuilder.DropIndex(
                name: "IX_orders_payment_id",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "payment_id",
                table: "orders");

            migrationBuilder.CreateIndex(
                name: "IX_payments_order_id",
                table: "payments",
                column: "order_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_payments_order_id",
                table: "payments");

            migrationBuilder.AddColumn<int>(
                name: "payment_id",
                table: "orders",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_payments_order_id",
                table: "payments",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "IX_orders_payment_id",
                table: "orders",
                column: "payment_id");

            migrationBuilder.AddForeignKey(
                name: "FK_orders_payments_payment_id",
                table: "orders",
                column: "payment_id",
                principalTable: "payments",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
