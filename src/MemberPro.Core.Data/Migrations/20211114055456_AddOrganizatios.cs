using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MemberPro.Core.Data.Migrations
{
    public partial class AddOrganizatios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_member_division_division_id",
                table: "member");

            migrationBuilder.DropForeignKey(
                name: "fk_member_region_region_id",
                table: "member");

            migrationBuilder.DropTable(
                name: "division");

            migrationBuilder.DropTable(
                name: "region");

            migrationBuilder.DropIndex(
                name: "ix_member_division_id",
                table: "member");

            migrationBuilder.DropColumn(
                name: "division_id",
                table: "member");

            migrationBuilder.RenameColumn(
                name: "region_id",
                table: "member",
                newName: "organization_id");

            migrationBuilder.RenameIndex(
                name: "ix_member_region_id",
                table: "member",
                newName: "ix_member_organization_id");

            migrationBuilder.CreateTable(
                name: "organization",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    parent_id = table.Column<int>(type: "integer", nullable: true),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    abbreviation = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_organization", x => x.id);
                    table.ForeignKey(
                        name: "fk_organization_organization_parent_id",
                        column: x => x.parent_id,
                        principalTable: "organization",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_organization_parent_id",
                table: "organization",
                column: "parent_id");

            migrationBuilder.Sql("UPDATE member SET organization_id = NULL");

            migrationBuilder.AddForeignKey(
                name: "fk_member_organization_organization_id",
                table: "member",
                column: "organization_id",
                principalTable: "organization",
                principalColumn: "id");

            migrationBuilder.InsertData("organization",
                columns: new[] { "id", "parent_id", "name", "abbreviation", "description", "created_on", "updated_on"},
                values: new object[,]
                {
                    { 1, null, "NMRA", "NMRA", null, new DateTime(2021, 11, 01, 0, 0, 0, DateTimeKind.Utc), new DateTime(2021, 11, 01, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, 1, "Midwest Region", "MWR", null, new DateTime(2021, 11, 01, 0, 0, 0, DateTimeKind.Utc), new DateTime(2021, 11, 01, 0, 0, 0, DateTimeKind.Utc) },
                    { 3, 2, "Central Indiana Division", "CIND", null, new DateTime(2021, 11, 01, 0, 0, 0, DateTimeKind.Utc), new DateTime(2021, 11, 01, 0, 0, 0, DateTimeKind.Utc) },
                    { 4, 2, "DuPage Division", "DUP", null, new DateTime(2021, 11, 01, 0, 0, 0, DateTimeKind.Utc), new DateTime(2021, 11, 01, 0, 0, 0, DateTimeKind.Utc) },
                    { 5, 2, "Fox Valley Division", "FXV", null, new DateTime(2021, 11, 01, 0, 0, 0, DateTimeKind.Utc), new DateTime(2021, 11, 01, 0, 0, 0, DateTimeKind.Utc) },
                    { 6, 2, "Illinois Terminal Division", "ILT", null, new DateTime(2021, 11, 01, 0, 0, 0, DateTimeKind.Utc), new DateTime(2021, 11, 01, 0, 0, 0, DateTimeKind.Utc) },
                    { 7, 2, "Illinois Valley Division", "ILV", null, new DateTime(2021, 11, 01, 0, 0, 0, DateTimeKind.Utc), new DateTime(2021, 11, 01, 0, 0, 0, DateTimeKind.Utc) },
                    { 8, 2, "Michiana Division", "MICH", null, new DateTime(2021, 11, 01, 0, 0, 0, DateTimeKind.Utc), new DateTime(2021, 11, 01, 0, 0, 0, DateTimeKind.Utc) },
                    { 9, 2, "Rock River Valley Division", "ROCKRIV", null, new DateTime(2021, 11, 01, 0, 0, 0, DateTimeKind.Utc), new DateTime(2021, 11, 01, 0, 0, 0, DateTimeKind.Utc) },
                    { 10, 2, "South Central Wisconsin Division", "SCWD", null, new DateTime(2021, 11, 01, 0, 0, 0, DateTimeKind.Utc), new DateTime(2021, 11, 01, 0, 0, 0, DateTimeKind.Utc) },
                    { 11, 2, "Winnebagoland Division", "WINNE", null, new DateTime(2021, 11, 01, 0, 0, 0, DateTimeKind.Utc), new DateTime(2021, 11, 01, 0, 0, 0, DateTimeKind.Utc) },
                    { 12, 2, "Wisconsin Southeastern Division", "WISE", null, new DateTime(2021, 11, 01, 0, 0, 0, DateTimeKind.Utc), new DateTime(2021, 11, 01, 0, 0, 0, DateTimeKind.Utc) },
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_member_organization_organization_id",
                table: "member");

            migrationBuilder.DropTable(
                name: "organization");

            migrationBuilder.RenameColumn(
                name: "organization_id",
                table: "member",
                newName: "region_id");

            migrationBuilder.RenameIndex(
                name: "ix_member_organization_id",
                table: "member",
                newName: "ix_member_region_id");

            migrationBuilder.AddColumn<int>(
                name: "division_id",
                table: "member",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "region",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    abbreviation = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_region", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "division",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    region_id = table.Column<int>(type: "integer", nullable: false),
                    abbreviation = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_division", x => x.id);
                    table.ForeignKey(
                        name: "fk_division_region_region_id",
                        column: x => x.region_id,
                        principalTable: "region",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_member_division_id",
                table: "member",
                column: "division_id");

            migrationBuilder.CreateIndex(
                name: "ix_division_region_id",
                table: "division",
                column: "region_id");

            migrationBuilder.AddForeignKey(
                name: "fk_member_division_division_id",
                table: "member",
                column: "division_id",
                principalTable: "division",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_member_region_region_id",
                table: "member",
                column: "region_id",
                principalTable: "region",
                principalColumn: "id");
        }
    }
}
