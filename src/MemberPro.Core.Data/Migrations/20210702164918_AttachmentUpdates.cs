using Microsoft.EntityFrameworkCore.Migrations;

namespace MemberPro.Core.Data.Migrations
{
    public partial class AttachmentUpdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "original_file_name",
                table: "attachment");

            migrationBuilder.RenameColumn(
                name: "saved_file_name",
                table: "attachment",
                newName: "file_name");

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "attachment",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "description",
                table: "attachment");

            migrationBuilder.RenameColumn(
                name: "file_name",
                table: "attachment",
                newName: "saved_file_name");

            migrationBuilder.AddColumn<string>(
                name: "original_file_name",
                table: "attachment",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }
    }
}
