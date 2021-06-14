using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MemberPro.Core.Data.Migrations
{
    public partial class MemberAchievementUpdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_member_achievement_member_approved_by_member_id",
                table: "member_achievement");

            migrationBuilder.DropIndex(
                name: "ix_member_achievement_approved_by_member_id",
                table: "member_achievement");

            migrationBuilder.DropColumn(
                name: "approved_by_member_id",
                table: "member_achievement");

            migrationBuilder.DropColumn(
                name: "approved_on",
                table: "member_achievement");

            migrationBuilder.RenameColumn(
                name: "submitted_on",
                table: "member_achievement",
                newName: "earned_on");

            migrationBuilder.AddColumn<int>(
                name: "created_by_member_id",
                table: "member_achievement",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "created_on",
                table: "member_achievement",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "ix_member_achievement_created_by_member_id",
                table: "member_achievement",
                column: "created_by_member_id");

            migrationBuilder.AddForeignKey(
                name: "fk_member_achievement_member_created_by_member_id",
                table: "member_achievement",
                column: "created_by_member_id",
                principalTable: "member",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_member_achievement_member_created_by_member_id",
                table: "member_achievement");

            migrationBuilder.DropIndex(
                name: "ix_member_achievement_created_by_member_id",
                table: "member_achievement");

            migrationBuilder.DropColumn(
                name: "created_by_member_id",
                table: "member_achievement");

            migrationBuilder.DropColumn(
                name: "created_on",
                table: "member_achievement");

            migrationBuilder.RenameColumn(
                name: "earned_on",
                table: "member_achievement",
                newName: "submitted_on");

            migrationBuilder.AddColumn<int>(
                name: "approved_by_member_id",
                table: "member_achievement",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "approved_on",
                table: "member_achievement",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_member_achievement_approved_by_member_id",
                table: "member_achievement",
                column: "approved_by_member_id");

            migrationBuilder.AddForeignKey(
                name: "fk_member_achievement_member_approved_by_member_id",
                table: "member_achievement",
                column: "approved_by_member_id",
                principalTable: "member",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
