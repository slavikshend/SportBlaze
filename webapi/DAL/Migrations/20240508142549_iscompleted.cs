using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webapi.Migrations
{
    /// <inheritdoc />
    public partial class iscompleted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "payment_amount",
                table: "payments");

            migrationBuilder.AddColumn<bool>(
                name: "is_completed",
                table: "payments",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_completed",
                table: "payments");

            migrationBuilder.AddColumn<decimal>(
                name: "payment_amount",
                table: "payments",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
