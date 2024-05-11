using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webapi.Migrations
{
    /// <inheritdoc />
    public partial class log_timestamp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "timestamp",
                table: "loglines",
                newName: "log_timestamp");

            migrationBuilder.RenameColumn(
                name: "loglevel",
                table: "loglines",
                newName: "log_level");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "log_timestamp",
                table: "loglines",
                newName: "timestamp");

            migrationBuilder.RenameColumn(
                name: "log_level",
                table: "loglines",
                newName: "loglevel");
        }
    }
}
