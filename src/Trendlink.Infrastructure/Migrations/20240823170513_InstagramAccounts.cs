using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trendlink.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InstagramAccounts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "account_type", table: "users");

            migrationBuilder.AddColumn<string>(
                name: "profile_picture",
                table: "users",
                type: "text",
                nullable: true
            );

            migrationBuilder.CreateTable(
                name: "instagram_accounts",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    facebook_page_id = table.Column<string>(type: "text", nullable: false),
                    metadata_id = table.Column<string>(type: "text", nullable: true),
                    metadata_ig_id = table.Column<long>(type: "bigint", nullable: false),
                    metadata_user_name = table.Column<string>(type: "text", nullable: false),
                    metadata_followers_count = table.Column<int>(type: "integer", nullable: false),
                    metadata_media_count = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_instagram_accounts", x => x.id);
                    table.ForeignKey(
                        name: "fk_instagram_accounts_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id"
                    );
                }
            );

            migrationBuilder.CreateIndex(
                name: "ix_instagram_accounts_user_id",
                table: "instagram_accounts",
                column: "user_id",
                unique: true
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "instagram_accounts");

            migrationBuilder.DropColumn(name: "profile_picture", table: "users");

            migrationBuilder.AddColumn<int>(
                name: "account_type",
                table: "users",
                type: "integer",
                nullable: false,
                defaultValue: 0
            );
        }
    }
}
