using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webapi.Migrations
{
    /// <inheritdoc />
    public partial class ChangedPasswordAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "email",
                keyValue: "admin@example.com",
                column: "hashed_password",
                value: "2ab9f9c0357ff177048e0f0d3ae2129982f279d1d90b0900b5bd3fb73f9bb00d");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "email",
                keyValue: "admin@example.com",
                column: "hashed_password",
                value: "3618124d4ffcf5e6625095cbcaf7ce572f406e92c01850f9be488881768abb70");
        }
    }
}
