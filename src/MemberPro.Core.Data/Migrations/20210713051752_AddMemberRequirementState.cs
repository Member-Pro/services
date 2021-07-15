using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MemberPro.Core.Data.Migrations
{
    public partial class AddMemberRequirementState : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "member_requirement_state",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    member_id = table.Column<int>(type: "integer", nullable: false),
                    requirement_id = table.Column<int>(type: "integer", nullable: false),
                    updated_on = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    data = table.Column<object>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_member_requirement_state", x => x.id);
                    table.ForeignKey(
                        name: "fk_member_requirement_state_member_member_id",
                        column: x => x.member_id,
                        principalTable: "member",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_member_requirement_state_requirement_requirement_id",
                        column: x => x.requirement_id,
                        principalTable: "requirement",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_member_requirement_state_member_id",
                table: "member_requirement_state",
                column: "member_id");

            migrationBuilder.CreateIndex(
                name: "ix_member_requirement_state_requirement_id",
                table: "member_requirement_state",
                column: "requirement_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "member_requirement_state");
        }
    }
}
