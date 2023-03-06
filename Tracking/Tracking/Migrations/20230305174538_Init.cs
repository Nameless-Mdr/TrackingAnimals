using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Tracking.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "info");

            migrationBuilder.EnsureSchema(
                name: "auth");

            migrationBuilder.CreateTable(
                name: "locations",
                schema: "info",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    latitude = table.Column<double>(type: "double precision", nullable: false),
                    longitude = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_locations", x => x.id);
                    table.CheckConstraint("CH_latitude", "latitude >= -90 AND latitude <= 90");
                    table.CheckConstraint("CH_longitude", "longitude >= -180 AND longitude <= 180");
                });

            migrationBuilder.CreateTable(
                name: "types",
                schema: "info",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name_type = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_types", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                schema: "auth",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    first_name = table.Column<string>(type: "text", nullable: false),
                    last_name = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "animals",
                schema: "info",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    weight = table.Column<double>(type: "double precision", nullable: false),
                    length = table.Column<double>(type: "double precision", nullable: false),
                    height = table.Column<double>(type: "double precision", nullable: false),
                    gender = table.Column<string>(type: "text", nullable: false),
                    life_status = table.Column<string>(type: "text", nullable: false, defaultValue: "ALIVE"),
                    chipper_id = table.Column<int>(type: "integer", nullable: false),
                    chipping_location_id = table.Column<long>(type: "bigint", nullable: false),
                    chipping_date_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    death_date_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_animals", x => x.id);
                    table.CheckConstraint("CH_gender", "gender IN ('MALE', 'FEMALE', 'OTHER')");
                    table.CheckConstraint("CH_height", "height > 0");
                    table.CheckConstraint("CH_length", "length > 0");
                    table.CheckConstraint("CH_life_status", "life_status IN ('ALIVE', 'DEAD')");
                    table.CheckConstraint("CH_weight", "weight > 0");
                    table.ForeignKey(
                        name: "FK_animals_locations_chipping_location_id",
                        column: x => x.chipping_location_id,
                        principalSchema: "info",
                        principalTable: "locations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_animals_users_chipper_id",
                        column: x => x.chipper_id,
                        principalSchema: "auth",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "user_session",
                schema: "auth",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    refresh_token = table.Column<Guid>(type: "uuid", nullable: false),
                    created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_session", x => x.id);
                    table.ForeignKey(
                        name: "FK_user_session_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "auth",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "types_animals",
                schema: "info",
                columns: table => new
                {
                    AnimalsId = table.Column<long>(type: "bigint", nullable: false),
                    TypesId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_types_animals", x => new { x.AnimalsId, x.TypesId });
                    table.ForeignKey(
                        name: "FK_types_animals_animals_AnimalsId",
                        column: x => x.AnimalsId,
                        principalSchema: "info",
                        principalTable: "animals",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_types_animals_types_TypesId",
                        column: x => x.TypesId,
                        principalSchema: "info",
                        principalTable: "types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_animals_chipper_id",
                schema: "info",
                table: "animals",
                column: "chipper_id");

            migrationBuilder.CreateIndex(
                name: "IX_animals_chipping_location_id",
                schema: "info",
                table: "animals",
                column: "chipping_location_id");

            migrationBuilder.CreateIndex(
                name: "IX_locations_latitude_longitude",
                schema: "info",
                table: "locations",
                columns: new[] { "latitude", "longitude" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_types_name_type",
                schema: "info",
                table: "types",
                column: "name_type",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_types_animals_TypesId",
                schema: "info",
                table: "types_animals",
                column: "TypesId");

            migrationBuilder.CreateIndex(
                name: "IX_user_session_user_id",
                schema: "auth",
                table: "user_session",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_users_email",
                schema: "auth",
                table: "users",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "types_animals",
                schema: "info");

            migrationBuilder.DropTable(
                name: "user_session",
                schema: "auth");

            migrationBuilder.DropTable(
                name: "animals",
                schema: "info");

            migrationBuilder.DropTable(
                name: "types",
                schema: "info");

            migrationBuilder.DropTable(
                name: "locations",
                schema: "info");

            migrationBuilder.DropTable(
                name: "users",
                schema: "auth");
        }
    }
}
