using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MemberPro.Core.Data.Migrations
{
    public partial class AddAchievementActivityRecord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "achievement_activity_record",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    achievement_id = table.Column<int>(type: "integer", nullable: false),
                    requirement_id = table.Column<int>(type: "integer", nullable: false),
                    member_id = table.Column<int>(type: "integer", nullable: false),
                    activity_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    quantity_completed = table.Column<decimal>(type: "numeric", nullable: true),
                    comments = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_achievement_activity_record", x => x.id);
                    table.ForeignKey(
                        name: "fk_achievement_activity_record_achievement_achievement_id",
                        column: x => x.achievement_id,
                        principalTable: "achievement",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_achievement_activity_record_achievement_requirement_require",
                        column: x => x.requirement_id,
                        principalTable: "achievement_requirement",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_achievement_activity_record_member_member_id",
                        column: x => x.member_id,
                        principalTable: "member",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "ix_achievement_activity_record_achievement_id",
                table: "achievement_activity_record",
                column: "achievement_id");

            migrationBuilder.CreateIndex(
                name: "ix_achievement_activity_record_member_id",
                table: "achievement_activity_record",
                column: "member_id");

            migrationBuilder.CreateIndex(
                name: "ix_achievement_activity_record_requirement_id",
                table: "achievement_activity_record",
                column: "requirement_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "achievement_activity_record");
        }
    }
}
