using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trendlink.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Reviews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "reviews",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    advertisement_id = table.Column<Guid>(type: "uuid", nullable: false),
                    cooperation_id = table.Column<Guid>(type: "uuid", nullable: false),
                    comment = table.Column<string>(type: "text", nullable: false),
                    rating = table.Column<int>(type: "integer", nullable: false),
                    created_on_utc = table.Column<DateTime>(
                        type: "timestamp with time zone",
                        nullable: false
                    )
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_reviews", x => x.id);
                    table.ForeignKey(
                        name: "fk_reviews_advertisements_advertisement_id",
                        column: x => x.advertisement_id,
                        principalTable: "advertisements",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "fk_reviews_cooperations_cooperation_id",
                        column: x => x.cooperation_id,
                        principalTable: "cooperations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "fk_reviews_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateIndex(
                name: "ix_reviews_advertisement_id",
                table: "reviews",
                column: "advertisement_id"
            );

            migrationBuilder.CreateIndex(
                name: "ix_reviews_cooperation_id",
                table: "reviews",
                column: "cooperation_id"
            );

            migrationBuilder.CreateIndex(name: "ix_reviews_id", table: "reviews", column: "id");

            migrationBuilder.CreateIndex(
                name: "ix_reviews_user_id",
                table: "reviews",
                column: "user_id"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "reviews");
        }
    }
}
