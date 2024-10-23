using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trendlink.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedIdForUserProfilePhoto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "profile_picture",
                table: "users",
                newName: "profile_picture_uri");

            migrationBuilder.AddColumn<string>(
                name: "profile_picture_id",
                table: "users",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "profile_picture_id",
                table: "users");

            migrationBuilder.RenameColumn(
                name: "profile_picture_uri",
                table: "users",
                newName: "profile_picture");
        }
    }
}
