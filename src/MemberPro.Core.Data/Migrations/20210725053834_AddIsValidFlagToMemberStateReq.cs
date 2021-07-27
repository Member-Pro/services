using Microsoft.EntityFrameworkCore.Migrations;

namespace MemberPro.Core.Data.Migrations
{
    public partial class AddIsValidFlagToMemberStateReq : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_valid",
                table: "member_requirement_state",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_valid",
                table: "member_requirement_state");
        }
    }
}
