using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trendlink.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class States_ColumnRename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "fk_states_countries_county_id", table: "states");

            migrationBuilder.RenameColumn(
                name: "county_id",
                table: "states",
                newName: "country_id"
            );

            migrationBuilder.RenameIndex(
                name: "ix_states_county_id",
                table: "states",
                newName: "ix_states_country_id"
            );

            migrationBuilder.AddForeignKey(
                name: "fk_states_countries_country_id",
                table: "states",
                column: "country_id",
                principalTable: "countries",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_states_countries_country_id",
                table: "states"
            );

            migrationBuilder.RenameColumn(
                name: "country_id",
                table: "states",
                newName: "county_id"
            );

            migrationBuilder.RenameIndex(
                name: "ix_states_country_id",
                table: "states",
                newName: "ix_states_county_id"
            );

            migrationBuilder.AddForeignKey(
                name: "fk_states_countries_county_id",
                table: "states",
                column: "county_id",
                principalTable: "countries",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade
            );
        }
    }
}
