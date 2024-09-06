using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trendlink.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Added_ExpiresAt_for_UserTokens : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_conditions_users_user_id",
                table: "conditions"
            );

            migrationBuilder.DropForeignKey(
                name: "fk_instagram_accounts_users_user_id",
                table: "instagram_accounts"
            );

            migrationBuilder.DropForeignKey(
                name: "fk_user_tokens_users_user_id",
                table: "user_tokens"
            );

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "expires_at_utc",
                table: "user_tokens",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(
                    new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                    new TimeSpan(0, 0, 0, 0, 0)
                )
            );

            migrationBuilder.AddForeignKey(
                name: "fk_conditions_users_user_id",
                table: "conditions",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade
            );

            migrationBuilder.AddForeignKey(
                name: "fk_instagram_accounts_users_user_id",
                table: "instagram_accounts",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade
            );

            migrationBuilder.AddForeignKey(
                name: "fk_user_tokens_users_user_id",
                table: "user_tokens",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_conditions_users_user_id",
                table: "conditions"
            );

            migrationBuilder.DropForeignKey(
                name: "fk_instagram_accounts_users_user_id",
                table: "instagram_accounts"
            );

            migrationBuilder.DropForeignKey(
                name: "fk_user_tokens_users_user_id",
                table: "user_tokens"
            );

            migrationBuilder.DropColumn(name: "expires_at_utc", table: "user_tokens");

            migrationBuilder.AddForeignKey(
                name: "fk_conditions_users_user_id",
                table: "conditions",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id"
            );

            migrationBuilder.AddForeignKey(
                name: "fk_instagram_accounts_users_user_id",
                table: "instagram_accounts",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id"
            );

            migrationBuilder.AddForeignKey(
                name: "fk_user_tokens_users_user_id",
                table: "user_tokens",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id"
            );
        }
    }
}
