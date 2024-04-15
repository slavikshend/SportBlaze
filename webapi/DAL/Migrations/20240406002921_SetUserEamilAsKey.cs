using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace webapi.Migrations
{
    /// <inheritdoc />
    public partial class SetUserEamilAsKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_favourites_users_user_id",
                table: "favourites");

            migrationBuilder.DropForeignKey(
                name: "FK_feedbacks_users_user_id",
                table: "feedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_order_details_products_product_id",
                table: "order_details");

            migrationBuilder.DropForeignKey(
                name: "FK_orders_users_user_id",
                table: "orders");

            migrationBuilder.DropForeignKey(
                name: "FK_payments_users_user_id",
                table: "payments");

            migrationBuilder.DropForeignKey(
                name: "FK_role_assignment_users_user_id",
                table: "role_assignment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_users",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_role_assignment_user_id",
                table: "role_assignment");

            migrationBuilder.DropIndex(
                name: "IX_payments_user_id",
                table: "payments");

            migrationBuilder.DropIndex(
                name: "IX_orders_user_id",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "IX_feedbacks_user_id",
                table: "feedbacks");

            migrationBuilder.DropIndex(
                name: "IX_favourites_user_id",
                table: "favourites");

            migrationBuilder.DropColumn(
                name: "id",
                table: "users");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "role_assignment");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "payments");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "feedbacks");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "favourites");

            migrationBuilder.RenameColumn(
                name: "password",
                table: "users",
                newName: "city");

            migrationBuilder.RenameColumn(
                name: "product_id",
                table: "order_details",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_order_details_product_id",
                table: "order_details",
                newName: "IX_order_details_ProductId");

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "users",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "user_email",
                table: "role_assignment",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "user_email",
                table: "payments",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "user_email",
                table: "orders",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "user_email",
                table: "order_details",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "date",
                table: "feedbacks",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "user_email",
                table: "feedbacks",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "user_email",
                table: "favourites",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_users",
                table: "users",
                column: "email");

            migrationBuilder.CreateIndex(
                name: "IX_role_assignment_user_email",
                table: "role_assignment",
                column: "user_email");

            migrationBuilder.CreateIndex(
                name: "IX_payments_user_email",
                table: "payments",
                column: "user_email");

            migrationBuilder.CreateIndex(
                name: "IX_orders_user_email",
                table: "orders",
                column: "user_email");

            migrationBuilder.CreateIndex(
                name: "IX_feedbacks_user_email",
                table: "feedbacks",
                column: "user_email");

            migrationBuilder.CreateIndex(
                name: "IX_favourites_user_email",
                table: "favourites",
                column: "user_email");

            migrationBuilder.AddForeignKey(
                name: "FK_favourites_users_user_email",
                table: "favourites",
                column: "user_email",
                principalTable: "users",
                principalColumn: "email",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_feedbacks_users_user_email",
                table: "feedbacks",
                column: "user_email",
                principalTable: "users",
                principalColumn: "email",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_order_details_products_ProductId",
                table: "order_details",
                column: "ProductId",
                principalTable: "products",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_orders_users_user_email",
                table: "orders",
                column: "user_email",
                principalTable: "users",
                principalColumn: "email",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_payments_users_user_email",
                table: "payments",
                column: "user_email",
                principalTable: "users",
                principalColumn: "email",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_role_assignment_users_user_email",
                table: "role_assignment",
                column: "user_email",
                principalTable: "users",
                principalColumn: "email",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_favourites_users_user_email",
                table: "favourites");

            migrationBuilder.DropForeignKey(
                name: "FK_feedbacks_users_user_email",
                table: "feedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_order_details_products_ProductId",
                table: "order_details");

            migrationBuilder.DropForeignKey(
                name: "FK_orders_users_user_email",
                table: "orders");

            migrationBuilder.DropForeignKey(
                name: "FK_payments_users_user_email",
                table: "payments");

            migrationBuilder.DropForeignKey(
                name: "FK_role_assignment_users_user_email",
                table: "role_assignment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_users",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_role_assignment_user_email",
                table: "role_assignment");

            migrationBuilder.DropIndex(
                name: "IX_payments_user_email",
                table: "payments");

            migrationBuilder.DropIndex(
                name: "IX_orders_user_email",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "IX_feedbacks_user_email",
                table: "feedbacks");

            migrationBuilder.DropIndex(
                name: "IX_favourites_user_email",
                table: "favourites");

            migrationBuilder.DropColumn(
                name: "user_email",
                table: "role_assignment");

            migrationBuilder.DropColumn(
                name: "user_email",
                table: "payments");

            migrationBuilder.DropColumn(
                name: "user_email",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "user_email",
                table: "order_details");

            migrationBuilder.DropColumn(
                name: "date",
                table: "feedbacks");

            migrationBuilder.DropColumn(
                name: "user_email",
                table: "feedbacks");

            migrationBuilder.DropColumn(
                name: "user_email",
                table: "favourites");

            migrationBuilder.RenameColumn(
                name: "city",
                table: "users",
                newName: "password");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "order_details",
                newName: "product_id");

            migrationBuilder.RenameIndex(
                name: "IX_order_details_ProductId",
                table: "order_details",
                newName: "IX_order_details_product_id");

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "users",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<int>(
                name: "id",
                table: "users",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "user_id",
                table: "role_assignment",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "user_id",
                table: "payments",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "user_id",
                table: "orders",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "user_id",
                table: "feedbacks",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "user_id",
                table: "favourites",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_users",
                table: "users",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_role_assignment_user_id",
                table: "role_assignment",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_payments_user_id",
                table: "payments",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_orders_user_id",
                table: "orders",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_feedbacks_user_id",
                table: "feedbacks",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_favourites_user_id",
                table: "favourites",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_favourites_users_user_id",
                table: "favourites",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_feedbacks_users_user_id",
                table: "feedbacks",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_order_details_products_product_id",
                table: "order_details",
                column: "product_id",
                principalTable: "products",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_orders_users_user_id",
                table: "orders",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_payments_users_user_id",
                table: "payments",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_role_assignment_users_user_id",
                table: "role_assignment",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
