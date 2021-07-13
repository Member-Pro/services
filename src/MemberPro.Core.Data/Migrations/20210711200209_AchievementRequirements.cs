using MemberPro.Core.Entities.Achievements;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MemberPro.Core.Data.Migrations
{
    public partial class AchievementRequirements : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "requirements",
                table: "achievement_component");

            migrationBuilder.CreateTable(
                name: "requirement",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    component_id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    validator_type_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    validation_parameters = table.Column<object>(type: "jsonb", nullable: true),
                    type = table.Column<int>(type: "integer", nullable: false),
                    min_count = table.Column<decimal>(type: "numeric", nullable: true),
                    max_count = table.Column<decimal>(type: "numeric", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_requirement", x => x.id);
                    table.ForeignKey(
                        name: "fk_requirement_achievement_component_component_id",
                        column: x => x.component_id,
                        principalTable: "achievement_component",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_requirement_component_id",
                table: "requirement",
                column: "component_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "requirement");

            migrationBuilder.AddColumn<Requirement[]>(
                name: "requirements",
                table: "achievement_component",
                type: "jsonb",
                nullable: true);
        }
    }
}
