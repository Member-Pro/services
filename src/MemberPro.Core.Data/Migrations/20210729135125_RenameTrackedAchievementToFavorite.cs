using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MemberPro.Core.Data.Migrations
{
    public partial class RenameTrackedAchievementToFavorite : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tracked_achievement");

            migrationBuilder.CreateTable(
                name: "favorite_achievement",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    member_id = table.Column<int>(type: "integer", nullable: false),
                    achievement_id = table.Column<int>(type: "integer", nullable: false),
                    notes = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    created_on = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_favorite_achievement", x => x.id);
                    table.ForeignKey(
                        name: "fk_favorite_achievement_achievement_achievement_id",
                        column: x => x.achievement_id,
                        principalTable: "achievement",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_favorite_achievement_member_member_id",
                        column: x => x.member_id,
                        principalTable: "member",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_favorite_achievement_achievement_id",
                table: "favorite_achievement",
                column: "achievement_id");

            migrationBuilder.CreateIndex(
                name: "ix_favorite_achievement_member_id",
                table: "favorite_achievement",
                column: "member_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "favorite_achievement");

            migrationBuilder.CreateTable(
                name: "tracked_achievement",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    achievement_id = table.Column<int>(type: "integer", nullable: false),
                    created_on = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    member_id = table.Column<int>(type: "integer", nullable: false),
                    notes = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tracked_achievement", x => x.id);
                    table.ForeignKey(
                        name: "fk_tracked_achievement_achievement_achievement_id",
                        column: x => x.achievement_id,
                        principalTable: "achievement",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_tracked_achievement_member_member_id",
                        column: x => x.member_id,
                        principalTable: "member",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_tracked_achievement_achievement_id",
                table: "tracked_achievement",
                column: "achievement_id");

            migrationBuilder.CreateIndex(
                name: "ix_tracked_achievement_member_id",
                table: "tracked_achievement",
                column: "member_id");
        }
    }
}
