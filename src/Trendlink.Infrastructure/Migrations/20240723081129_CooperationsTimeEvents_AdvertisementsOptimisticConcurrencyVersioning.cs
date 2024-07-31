using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trendlink.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CooperationsTimeEvents_AdvertisementsOptimisticConcurrencyVersioning
        : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "cancelled_on_utc",
                table: "cooperations",
                type: "timestamp with time zone",
                nullable: true
            );

            migrationBuilder.AddColumn<DateTime>(
                name: "completed_on_utc",
                table: "cooperations",
                type: "timestamp with time zone",
                nullable: true
            );

            migrationBuilder.AddColumn<DateTime>(
                name: "confirmed_on_utc",
                table: "cooperations",
                type: "timestamp with time zone",
                nullable: true
            );

            migrationBuilder.AddColumn<DateTime>(
                name: "pended_on_utc",
                table: "cooperations",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
            );

            migrationBuilder.AddColumn<DateTime>(
                name: "rejected_on_utc",
                table: "cooperations",
                type: "timestamp with time zone",
                nullable: true
            );

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                table: "advertisements",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "cancelled_on_utc", table: "cooperations");

            migrationBuilder.DropColumn(name: "completed_on_utc", table: "cooperations");

            migrationBuilder.DropColumn(name: "confirmed_on_utc", table: "cooperations");

            migrationBuilder.DropColumn(name: "pended_on_utc", table: "cooperations");

            migrationBuilder.DropColumn(name: "rejected_on_utc", table: "cooperations");

            migrationBuilder.DropColumn(name: "xmin", table: "advertisements");
        }
    }
}
