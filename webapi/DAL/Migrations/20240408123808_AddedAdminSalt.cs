using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webapi.Migrations
{
    /// <inheritdoc />
    public partial class AddedAdminSalt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "email",
                keyValue: "admin@example.com",
                columns: new[] { "hashed_password", "salt" },
                values: new object[] { "3618124d4ffcf5e6625095cbcaf7ce572f406e92c01850f9be488881768abb70", "adminsalt" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "email",
                keyValue: "admin@example.com",
                columns: new[] { "hashed_password", "salt" },
                values: new object[] { "c0080afeb2e67fc26ab6f796e7e8931e0b40f07e448f234ce88de80429932222", null });
        }
    }
}
