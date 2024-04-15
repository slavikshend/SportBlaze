using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webapi.Migrations
{
    /// <inheritdoc />
    public partial class RedidSalt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "email",
                keyValue: "admin@example.com",
                columns: new[] { "hashed_password", "salt" },
                values: new object[] { "bdf936f8accef5325b7511db5df35d6520691ab268ed758a0759264ce52cb475", "8YT+1d99ucFMdmAJEXeX3g==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "email",
                keyValue: "admin@example.com",
                columns: new[] { "hashed_password", "salt" },
                values: new object[] { "2ab9f9c0357ff177048e0f0d3ae2129982f279d1d90b0900b5bd3fb73f9bb00d", "adminsalt" });
        }
    }
}
