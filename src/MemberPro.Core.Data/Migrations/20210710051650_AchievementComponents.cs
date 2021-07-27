using System;
using MemberPro.Core.Entities.Achievements;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MemberPro.Core.Data.Migrations
{
    public partial class AchievementComponents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_achievement_activity_achievement_requirement_requirement_id",
                table: "achievement_activity");

            migrationBuilder.DropTable(
                name: "achievement_requirement");

            migrationBuilder.DropIndex(
                name: "ix_achievement_activity_requirement_id",
                table: "achievement_activity");

            migrationBuilder.DropColumn(
                name: "requirement_id",
                table: "achievement_activity");

            migrationBuilder.AddColumn<int>(
                name: "component_id",
                table: "achievement_activity",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "achievement_component",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    achievement_id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    is_disabled = table.Column<bool>(type: "boolean", nullable: false),
                    created_on = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_on = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    requirements = table.Column<Requirement[]>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_achievement_component", x => x.id);
                    table.ForeignKey(
                        name: "fk_achievement_component_achievement_achievement_id",
                        column: x => x.achievement_id,
                        principalTable: "achievement",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_achievement_activity_component_id",
                table: "achievement_activity",
                column: "component_id");

            migrationBuilder.CreateIndex(
                name: "ix_achievement_component_achievement_id",
                table: "achievement_component",
                column: "achievement_id");

            migrationBuilder.AddForeignKey(
                name: "fk_achievement_activity_achievement_component_component_id",
                table: "achievement_activity",
                column: "component_id",
                principalTable: "achievement_component",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_achievement_activity_achievement_component_component_id",
                table: "achievement_activity");

            migrationBuilder.DropTable(
                name: "achievement_component");

            migrationBuilder.DropIndex(
                name: "ix_achievement_activity_component_id",
                table: "achievement_activity");

            migrationBuilder.DropColumn(
                name: "component_id",
                table: "achievement_activity");

            migrationBuilder.AddColumn<int>(
                name: "requirement_id",
                table: "achievement_activity",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "achievement_requirement",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    achievement_id = table.Column<int>(type: "integer", nullable: false),
                    created_on = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    is_disabled = table.Column<bool>(type: "boolean", nullable: false),
                    is_required = table.Column<bool>(type: "boolean", nullable: false),
                    minimum_count = table.Column<int>(type: "integer", nullable: true),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    updated_on = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_achievement_requirement", x => x.id);
                    table.ForeignKey(
                        name: "fk_achievement_requirement_achievement_achievement_id",
                        column: x => x.achievement_id,
                        principalTable: "achievement",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_achievement_activity_requirement_id",
                table: "achievement_activity",
                column: "requirement_id");

            migrationBuilder.CreateIndex(
                name: "ix_achievement_requirement_achievement_id",
                table: "achievement_requirement",
                column: "achievement_id");

            migrationBuilder.AddForeignKey(
                name: "fk_achievement_activity_achievement_requirement_requirement_id",
                table: "achievement_activity",
                column: "requirement_id",
                principalTable: "achievement_requirement",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
