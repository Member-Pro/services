using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MemberPro.Core.Data.Migrations
{
    public partial class OrganizationOfficers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "officer_position",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    organization_id = table.Column<int>(type: "integer", nullable: false),
                    title = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    position_type = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_officer_position", x => x.id);
                    table.ForeignKey(
                        name: "fk_officer_position_organization_organization_id",
                        column: x => x.organization_id,
                        principalTable: "organization",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "officer",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    position_id = table.Column<int>(type: "integer", nullable: false),
                    member_id = table.Column<int>(type: "integer", nullable: false),
                    term_start = table.Column<DateOnly>(type: "date", nullable: true),
                    term_end = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_officer", x => x.id);
                    table.ForeignKey(
                        name: "fk_officer_member_member_id",
                        column: x => x.member_id,
                        principalTable: "member",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_officer_officer_position_position_id",
                        column: x => x.position_id,
                        principalTable: "officer_position",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_officer_member_id",
                table: "officer",
                column: "member_id");

            migrationBuilder.CreateIndex(
                name: "ix_officer_position_id",
                table: "officer",
                column: "position_id");

            migrationBuilder.CreateIndex(
                name: "ix_officer_position_organization_id",
                table: "officer_position",
                column: "organization_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "officer");

            migrationBuilder.DropTable(
                name: "officer_position");
        }
    }
}
