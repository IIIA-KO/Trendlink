using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trendlink.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_EmailVerificationToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "email_verified",
                table: "users",
                type: "boolean",
                nullable: false,
                defaultValue: false
            );

            migrationBuilder.CreateTable(
                name: "email_verification_tokens",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_on_utc = table.Column<DateTime>(
                        type: "timestamp with time zone",
                        nullable: false
                    ),
                    expires_at_utc = table.Column<DateTime>(
                        type: "timestamp with time zone",
                        nullable: false
                    )
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_email_verification_tokens", x => x.id);
                    table.ForeignKey(
                        name: "fk_email_verification_tokens_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateIndex(
                name: "ix_email_verification_tokens_user_id",
                table: "email_verification_tokens",
                column: "user_id"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "email_verification_tokens");

            migrationBuilder.DropColumn(name: "email_verified", table: "users");
        }
    }
}
