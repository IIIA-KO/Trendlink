using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trendlink.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Configured_Cooperations_OnDeleteBehavior : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_cooperations_users_buyer_id",
                table: "cooperations");

            migrationBuilder.DropForeignKey(
                name: "fk_cooperations_users_seller_id",
                table: "cooperations");

            migrationBuilder.AddForeignKey(
                name: "fk_cooperations_users_buyer_id",
                table: "cooperations",
                column: "buyer_id",
                principalTable: "users",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_cooperations_users_seller_id",
                table: "cooperations",
                column: "seller_id",
                principalTable: "users",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_cooperations_users_buyer_id",
                table: "cooperations");

            migrationBuilder.DropForeignKey(
                name: "fk_cooperations_users_seller_id",
                table: "cooperations");

            migrationBuilder.AddForeignKey(
                name: "fk_cooperations_users_buyer_id",
                table: "cooperations",
                column: "buyer_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_cooperations_users_seller_id",
                table: "cooperations",
                column: "seller_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
