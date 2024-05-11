using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webapi.Migrations
{
    /// <inheritdoc />
    public partial class payment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orders_payment_methods_payment_method_id",
                table: "orders");

            migrationBuilder.DropForeignKey(
                name: "FK_payments_users_user_email",
                table: "payments");

            migrationBuilder.RenameColumn(
                name: "user_email",
                table: "payments",
                newName: "UserEmail");

            migrationBuilder.RenameIndex(
                name: "IX_payments_user_email",
                table: "payments",
                newName: "IX_payments_UserEmail");

            migrationBuilder.RenameColumn(
                name: "payment_method_id",
                table: "orders",
                newName: "payment_id");

            migrationBuilder.RenameIndex(
                name: "IX_orders_payment_method_id",
                table: "orders",
                newName: "IX_orders_payment_id");

            migrationBuilder.AlterColumn<string>(
                name: "UserEmail",
                table: "payments",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<int>(
                name: "payment_method_id",
                table: "payments",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_payments_payment_method_id",
                table: "payments",
                column: "payment_method_id");

            migrationBuilder.AddForeignKey(
                name: "FK_orders_payments_payment_id",
                table: "orders",
                column: "payment_id",
                principalTable: "payments",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_payments_payment_methods_payment_method_id",
                table: "payments",
                column: "payment_method_id",
                principalTable: "payment_methods",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_payments_users_UserEmail",
                table: "payments",
                column: "UserEmail",
                principalTable: "users",
                principalColumn: "email");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orders_payments_payment_id",
                table: "orders");

            migrationBuilder.DropForeignKey(
                name: "FK_payments_payment_methods_payment_method_id",
                table: "payments");

            migrationBuilder.DropForeignKey(
                name: "FK_payments_users_UserEmail",
                table: "payments");

            migrationBuilder.DropIndex(
                name: "IX_payments_payment_method_id",
                table: "payments");

            migrationBuilder.DropColumn(
                name: "payment_method_id",
                table: "payments");

            migrationBuilder.RenameColumn(
                name: "UserEmail",
                table: "payments",
                newName: "user_email");

            migrationBuilder.RenameIndex(
                name: "IX_payments_UserEmail",
                table: "payments",
                newName: "IX_payments_user_email");

            migrationBuilder.RenameColumn(
                name: "payment_id",
                table: "orders",
                newName: "payment_method_id");

            migrationBuilder.RenameIndex(
                name: "IX_orders_payment_id",
                table: "orders",
                newName: "IX_orders_payment_method_id");

            migrationBuilder.AlterColumn<string>(
                name: "user_email",
                table: "payments",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_orders_payment_methods_payment_method_id",
                table: "orders",
                column: "payment_method_id",
                principalTable: "payment_methods",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_payments_users_user_email",
                table: "payments",
                column: "user_email",
                principalTable: "users",
                principalColumn: "email",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
