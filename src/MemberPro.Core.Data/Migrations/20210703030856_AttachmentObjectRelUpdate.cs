using Microsoft.EntityFrameworkCore.Migrations;

namespace MemberPro.Core.Data.Migrations
{
    public partial class AttachmentObjectRelUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "attachment_group",
                table: "attachment",
                newName: "object_type");

            migrationBuilder.AddColumn<int>(
                name: "object_id",
                table: "attachment",
                type: "integer",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "object_id",
                table: "attachment");

            migrationBuilder.RenameColumn(
                name: "object_type",
                table: "attachment",
                newName: "attachment_group");
        }
    }
}
