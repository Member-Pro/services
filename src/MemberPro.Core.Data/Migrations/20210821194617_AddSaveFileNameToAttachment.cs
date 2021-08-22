using Microsoft.EntityFrameworkCore.Migrations;

namespace MemberPro.Core.Data.Migrations
{
    public partial class AddSaveFileNameToAttachment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "save_file_name",
                table: "attachment",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.Sql(@"
                UPDATE attachment attach
                SET save_file_name = attach.file_name
                FROM attachment a
                WHERE a.id = attach.id
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "save_file_name",
                table: "attachment");
        }
    }
}
