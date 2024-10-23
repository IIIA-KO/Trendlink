using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trendlink.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenamePictureToPhoto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "profile_picture_uri",
                table: "users",
                newName: "profile_photo_uri");

            migrationBuilder.RenameColumn(
                name: "profile_picture_id",
                table: "users",
                newName: "profile_photo_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "profile_photo_uri",
                table: "users",
                newName: "profile_picture_uri");

            migrationBuilder.RenameColumn(
                name: "profile_photo_id",
                table: "users",
                newName: "profile_picture_id");
        }
    }
}
