using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trendlink.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedRating_to_User : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "fk_reviews_users_user_id", table: "reviews");

            migrationBuilder.RenameColumn(name: "user_id", table: "reviews", newName: "seller_id");

            migrationBuilder.RenameIndex(
                name: "ix_reviews_user_id",
                table: "reviews",
                newName: "ix_reviews_seller_id"
            );

            migrationBuilder.AddColumn<int>(
                name: "rating",
                table: "users",
                type: "integer",
                nullable: false,
                defaultValue: 0
            );

            migrationBuilder.AddColumn<Guid>(
                name: "buyer_id",
                table: "reviews",
                type: "uuid",
                nullable: false,
                defaultValue: Guid.Empty
            );

            migrationBuilder.CreateIndex(
                name: "ix_reviews_buyer_id",
                table: "reviews",
                column: "buyer_id"
            );

            migrationBuilder.AddForeignKey(
                name: "fk_reviews_users_buyer_id",
                table: "reviews",
                column: "buyer_id",
                principalTable: "users",
                principalColumn: "id"
            );

            migrationBuilder.AddForeignKey(
                name: "fk_reviews_users_seller_id",
                table: "reviews",
                column: "seller_id",
                principalTable: "users",
                principalColumn: "id"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "fk_reviews_users_buyer_id", table: "reviews");

            migrationBuilder.DropForeignKey(name: "fk_reviews_users_seller_id", table: "reviews");

            migrationBuilder.DropIndex(name: "ix_reviews_buyer_id", table: "reviews");

            migrationBuilder.DropColumn(name: "rating", table: "users");

            migrationBuilder.DropColumn(name: "buyer_id", table: "reviews");

            migrationBuilder.RenameColumn(name: "seller_id", table: "reviews", newName: "user_id");

            migrationBuilder.RenameIndex(
                name: "ix_reviews_seller_id",
                table: "reviews",
                newName: "ix_reviews_user_id"
            );

            migrationBuilder.AddForeignKey(
                name: "fk_reviews_users_user_id",
                table: "reviews",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade
            );
        }
    }
}
