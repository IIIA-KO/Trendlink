using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trendlink.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Rename_Cities_to_State : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "fk_users_cities_city_id", table: "users");

            migrationBuilder.DropTable(name: "cities");

            migrationBuilder.RenameColumn(name: "city_id", table: "users", newName: "state_id");

            migrationBuilder.RenameIndex(
                name: "ix_users_city_id",
                table: "users",
                newName: "ix_users_state_id"
            );

            migrationBuilder.CreateTable(
                name: "states",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(
                        type: "character varying(200)",
                        maxLength: 200,
                        nullable: false
                    ),
                    county_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_states", x => x.id);
                    table.ForeignKey(
                        name: "fk_states_countries_county_id",
                        column: x => x.county_id,
                        principalTable: "countries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateIndex(
                name: "ix_states_county_id",
                table: "states",
                column: "county_id"
            );

            migrationBuilder.AddForeignKey(
                name: "fk_users_states_state_id",
                table: "users",
                column: "state_id",
                principalTable: "states",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "fk_users_states_state_id", table: "users");

            migrationBuilder.DropTable(name: "states");

            migrationBuilder.RenameColumn(name: "state_id", table: "users", newName: "city_id");

            migrationBuilder.RenameIndex(
                name: "ix_users_state_id",
                table: "users",
                newName: "ix_users_city_id"
            );

            migrationBuilder.CreateTable(
                name: "cities",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    county_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(
                        type: "character varying(200)",
                        maxLength: 200,
                        nullable: false
                    )
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cities", x => x.id);
                    table.ForeignKey(
                        name: "fk_cities_countries_county_id",
                        column: x => x.county_id,
                        principalTable: "countries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateIndex(
                name: "ix_cities_county_id",
                table: "cities",
                column: "county_id"
            );

            migrationBuilder.AddForeignKey(
                name: "fk_users_cities_city_id",
                table: "users",
                column: "city_id",
                principalTable: "cities",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade
            );
        }
    }
}
