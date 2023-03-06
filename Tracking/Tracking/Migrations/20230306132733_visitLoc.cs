using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Tracking.Migrations
{
    /// <inheritdoc />
    public partial class visitLoc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "visit_locations",
                schema: "info",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    date_time_of_visit_location_point = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    animal_id = table.Column<long>(type: "bigint", nullable: false),
                    location_point_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_visit_locations", x => x.id);
                    table.ForeignKey(
                        name: "FK_visit_locations_animals_animal_id",
                        column: x => x.animal_id,
                        principalSchema: "info",
                        principalTable: "animals",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_visit_locations_locations_location_point_id",
                        column: x => x.location_point_id,
                        principalSchema: "info",
                        principalTable: "locations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_visit_locations_animal_id",
                schema: "info",
                table: "visit_locations",
                column: "animal_id");

            migrationBuilder.CreateIndex(
                name: "IX_visit_locations_location_point_id",
                schema: "info",
                table: "visit_locations",
                column: "location_point_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "visit_locations",
                schema: "info");
        }
    }
}
