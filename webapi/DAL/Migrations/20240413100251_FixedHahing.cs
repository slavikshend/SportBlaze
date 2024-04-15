using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webapi.Migrations
{
    /// <inheritdoc />
    public partial class FixedHahing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "email",
                keyValue: "admin@example.com",
                columns: new[] { "hashed_password", "salt" },
                values: new object[] { "eea64d928eefc66824117fd49f28463a1a4da3b5c3e170b6cd9159afdafaee56", "yuz1xllqhl7jcudb" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "email",
                keyValue: "admin@example.com",
                columns: new[] { "hashed_password", "salt" },
                values: new object[] { "bdf936f8accef5325b7511db5df35d6520691ab268ed758a0759264ce52cb475", "8YT+1d99ucFMdmAJEXeX3g==" });
        }
    }
}
