using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webapi.Migrations
{
    /// <inheritdoc />
    public partial class payment3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_payments_users_UserEmail",
                table: "payments");

            migrationBuilder.DropIndex(
                name: "IX_payments_UserEmail",
                table: "payments");

            migrationBuilder.DropColumn(
                name: "UserEmail",
                table: "payments");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                table: "payments",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_payments_UserEmail",
                table: "payments",
                column: "UserEmail");

            migrationBuilder.AddForeignKey(
                name: "FK_payments_users_UserEmail",
                table: "payments",
                column: "UserEmail",
                principalTable: "users",
                principalColumn: "email");
        }
    }
}
