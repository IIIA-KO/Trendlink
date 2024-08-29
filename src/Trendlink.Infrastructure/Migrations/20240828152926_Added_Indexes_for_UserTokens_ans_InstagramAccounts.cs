using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trendlink.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Added_Indexes_for_UserTokens_ans_InstagramAccounts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "ix_user_tokens_expires_at_utc",
                table: "user_tokens",
                column: "expires_at_utc");

            migrationBuilder.CreateIndex(
                name: "ix_instagram_accounts_last_updated_at_utc",
                table: "instagram_accounts",
                column: "last_updated_at_utc");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_user_tokens_expires_at_utc",
                table: "user_tokens");

            migrationBuilder.DropIndex(
                name: "ix_instagram_accounts_last_updated_at_utc",
                table: "instagram_accounts");
        }
    }
}
