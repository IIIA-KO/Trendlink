using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trendlink.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Added_UserId_FK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "user_id",
                table: "user_tokens",
                type: "uuid",
                nullable: false,
                defaultValue: Guid.NewGuid()
            );

            migrationBuilder.CreateIndex(
                name: "ix_user_tokens_user_id",
                table: "user_tokens",
                column: "user_id",
                unique: true
            );

            migrationBuilder.AddForeignKey(
                name: "fk_user_tokens_users_user_id",
                table: "user_tokens",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_user_tokens_users_user_id",
                table: "user_tokens"
            );

            migrationBuilder.DropIndex(name: "ix_user_tokens_user_id", table: "user_tokens");

            migrationBuilder.DropColumn(name: "user_id", table: "user_tokens");
        }
    }
}
