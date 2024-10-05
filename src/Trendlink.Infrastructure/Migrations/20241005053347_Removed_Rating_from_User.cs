using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trendlink.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Removed_Rating_from_User : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "rating",
                table: "users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "rating",
                table: "users",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
