using Microsoft.EntityFrameworkCore.Migrations;

namespace MemberPro.Core.Data.Migrations
{
    public partial class AddDisplayOrderProps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "display_order",
                table: "requirement",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "display_order",
                table: "achievement_component",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "display_order",
                table: "achievement",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "display_order",
                table: "requirement");

            migrationBuilder.DropColumn(
                name: "display_order",
                table: "achievement_component");

            migrationBuilder.DropColumn(
                name: "display_order",
                table: "achievement");
        }
    }
}
