using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MemberPro.Core.Data.Migrations
{
    public partial class MemberCurrentPlan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "current_plan_id",
                table: "member",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_member_current_plan_id",
                table: "member",
                column: "current_plan_id");

            migrationBuilder.AddForeignKey(
                name: "fk_member_membership_plan_current_plan_id",
                table: "member",
                column: "current_plan_id",
                principalTable: "membership_plan",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_member_membership_plan_current_plan_id",
                table: "member");

            migrationBuilder.DropIndex(
                name: "ix_member_current_plan_id",
                table: "member");

            migrationBuilder.DropColumn(
                name: "current_plan_id",
                table: "member");
        }
    }
}
