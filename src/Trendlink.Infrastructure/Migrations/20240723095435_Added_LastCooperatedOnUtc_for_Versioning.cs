using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trendlink.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Added_LastCooperatedOnUtc_for_Versioning : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "last_cooperated_on_utc",
                table: "advertisements",
                type: "timestamp with time zone",
                nullable: true
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "last_cooperated_on_utc", table: "advertisements");
        }
    }
}
