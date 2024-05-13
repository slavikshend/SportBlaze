using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webapi.Migrations
{
    /// <inheritdoc />
    public partial class mig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "email",
                keyValue: "admin@example.com",
                column: "hashed_password",
                value: "eea64d928eefc66824117fd49f28463a1a4da3b5c3e170b6cd9159afdafaee56");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "email",
                keyValue: "admin@example.com",
                column: "hashed_password",
                value: "ed647ee632f13df6c65f9e47929f13cfc35069bbaa70e50c157bac04575c4e37");
        }
    }
}
