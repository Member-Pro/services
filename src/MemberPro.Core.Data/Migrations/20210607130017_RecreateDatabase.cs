using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MemberPro.Core.Data.Migrations
{
    public partial class RecreateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "achievement",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    info_url = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    image_filename = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    is_disabled = table.Column<bool>(type: "boolean", nullable: false),
                    created_on = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_on = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_achievement", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "country",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    abbreviation = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_country", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "custom_field",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    display_order = table.Column<int>(type: "integer", nullable: false),
                    is_required = table.Column<bool>(type: "boolean", nullable: false),
                    field_type = table.Column<int>(type: "integer", nullable: false),
                    value_options = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_custom_field", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "membership_plan",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    sku = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    available_start_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    available_end_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    price = table.Column<decimal>(type: "numeric(8,2)", nullable: false),
                    duration_in_months = table.Column<int>(type: "integer", nullable: false),
                    created_on = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_on = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_membership_plan", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "region",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    abbreviation = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_region", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "achievement_step",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    achievement_id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    is_required = table.Column<bool>(type: "boolean", nullable: false),
                    minimum_count = table.Column<int>(type: "integer", nullable: true),
                    is_disabled = table.Column<bool>(type: "boolean", nullable: false),
                    created_on = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_on = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_achievement_step", x => x.id);
                    table.ForeignKey(
                        name: "fk_achievement_step_achievement_achievement_id",
                        column: x => x.achievement_id,
                        principalTable: "achievement",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "state_province",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    country_id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    abbreviation = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_state_province", x => x.id);
                    table.ForeignKey(
                        name: "fk_state_province_country_country_id",
                        column: x => x.country_id,
                        principalTable: "country",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "division",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    region_id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    abbreviation = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true)
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

            migrationBuilder.CreateTable(
                name: "member",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    subject_id = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    first_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    last_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    joined_on = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    email_address = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    date_of_birth = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    country_id = table.Column<int>(type: "integer", nullable: false),
                    state_province_id = table.Column<int>(type: "integer", nullable: false),
                    address = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    address2 = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    city = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    postal_code = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    phone_number = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    show_in_directory = table.Column<bool>(type: "boolean", nullable: false),
                    region_id = table.Column<int>(type: "integer", nullable: true),
                    division_id = table.Column<int>(type: "integer", nullable: true),
                    biography = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    interests = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_member", x => x.id);
                    table.ForeignKey(
                        name: "fk_member_country_country_id",
                        column: x => x.country_id,
                        principalTable: "country",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_member_division_division_id",
                        column: x => x.division_id,
                        principalTable: "division",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_member_region_region_id",
                        column: x => x.region_id,
                        principalTable: "region",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_member_state_province_state_province_id",
                        column: x => x.state_province_id,
                        principalTable: "state_province",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "member_achievement",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    member_id = table.Column<int>(type: "integer", nullable: false),
                    achievement_id = table.Column<int>(type: "integer", nullable: false),
                    submitted_on = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    approved_on = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    approved_by_member_id = table.Column<int>(type: "integer", nullable: true),
                    display_publicly = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_member_achievement", x => x.id);
                    table.ForeignKey(
                        name: "fk_member_achievement_achievement_achievement_id",
                        column: x => x.achievement_id,
                        principalTable: "achievement",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_member_achievement_member_approved_by_member_id",
                        column: x => x.approved_by_member_id,
                        principalTable: "member",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_member_achievement_member_member_id",
                        column: x => x.member_id,
                        principalTable: "member",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "member_achievement_progress",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    member_id = table.Column<int>(type: "integer", nullable: false),
                    achievement_id = table.Column<int>(type: "integer", nullable: false),
                    created_on = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    amount = table.Column<int>(type: "integer", nullable: false),
                    comments = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_member_achievement_progress", x => x.id);
                    table.ForeignKey(
                        name: "fk_member_achievement_progress_achievement_achievement_id",
                        column: x => x.achievement_id,
                        principalTable: "achievement",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_member_achievement_progress_member_member_id",
                        column: x => x.member_id,
                        principalTable: "member",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "member_custom_field_value",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    member_id = table.Column<int>(type: "integer", nullable: false),
                    field_id = table.Column<int>(type: "integer", nullable: false),
                    value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_member_custom_field_value", x => x.id);
                    table.ForeignKey(
                        name: "fk_member_custom_field_value_custom_field_field_id",
                        column: x => x.field_id,
                        principalTable: "custom_field",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_member_custom_field_value_member_member_id",
                        column: x => x.member_id,
                        principalTable: "member",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "member_renewal",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    member_id = table.Column<int>(type: "integer", nullable: false),
                    plan_id = table.Column<int>(type: "integer", nullable: false),
                    start_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    end_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    paid_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    paid_amount = table.Column<decimal>(type: "numeric", nullable: true),
                    transaction_id = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    comments = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_member_renewal", x => x.id);
                    table.ForeignKey(
                        name: "fk_member_renewal_member_member_id",
                        column: x => x.member_id,
                        principalTable: "member",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_member_renewal_membership_plan_plan_id",
                        column: x => x.plan_id,
                        principalTable: "membership_plan",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_achievement_step_achievement_id",
                table: "achievement_step",
                column: "achievement_id");

            migrationBuilder.CreateIndex(
                name: "ix_division_region_id",
                table: "division",
                column: "region_id");

            migrationBuilder.CreateIndex(
                name: "ix_member_country_id",
                table: "member",
                column: "country_id");

            migrationBuilder.CreateIndex(
                name: "ix_member_division_id",
                table: "member",
                column: "division_id");

            migrationBuilder.CreateIndex(
                name: "ix_member_region_id",
                table: "member",
                column: "region_id");

            migrationBuilder.CreateIndex(
                name: "ix_member_state_province_id",
                table: "member",
                column: "state_province_id");

            migrationBuilder.CreateIndex(
                name: "ix_member_achievement_achievement_id",
                table: "member_achievement",
                column: "achievement_id");

            migrationBuilder.CreateIndex(
                name: "ix_member_achievement_approved_by_member_id",
                table: "member_achievement",
                column: "approved_by_member_id");

            migrationBuilder.CreateIndex(
                name: "ix_member_achievement_member_id",
                table: "member_achievement",
                column: "member_id");

            migrationBuilder.CreateIndex(
                name: "ix_member_achievement_progress_achievement_id",
                table: "member_achievement_progress",
                column: "achievement_id");

            migrationBuilder.CreateIndex(
                name: "ix_member_achievement_progress_member_id",
                table: "member_achievement_progress",
                column: "member_id");

            migrationBuilder.CreateIndex(
                name: "ix_member_custom_field_value_field_id",
                table: "member_custom_field_value",
                column: "field_id");

            migrationBuilder.CreateIndex(
                name: "ix_member_custom_field_value_member_id",
                table: "member_custom_field_value",
                column: "member_id");

            migrationBuilder.CreateIndex(
                name: "ix_member_renewal_member_id",
                table: "member_renewal",
                column: "member_id");

            migrationBuilder.CreateIndex(
                name: "ix_member_renewal_plan_id",
                table: "member_renewal",
                column: "plan_id");

            migrationBuilder.CreateIndex(
                name: "ix_state_province_country_id",
                table: "state_province",
                column: "country_id");

                // SEED DATA

            migrationBuilder.InsertData("country",
                columns: new[] { "id", "name", "abbreviation" },
                values: new object[,]
                {
                    { 1, "United States", "US" },
                    { 2, "Canada", "CA" }
                });

            migrationBuilder.InsertData("state_province",
                columns: new[] { "country_id", "name", "abbreviation" },
                values: new object[,]
                {
                    {1, "Alabama","AL" },
                    {1, "Alaska","AK" },
                    {1, "Arizona","AZ" },
                    {1, "Arkansas","AR" },
                    {1, "California","CA" },
                    {1, "Colorado","CO" },
                    {1, "Connecticut","CT" },
                    {1, "Delaware","DE" },
                    {1, "District of Columbia","DC" },
                    {1, "Florida","FL" },
                    {1, "Georgia","GA" },
                    {1, "Hawaii","HI" },
                    {1, "Idaho","ID" },
                    {1, "Illinois","IL" },
                    {1, "Indiana","IN" },
                    {1, "Iowa","IA" },
                    {1, "Kansas","KS" },
                    {1, "Kentucky","KY" },
                    {1, "Louisiana","LA" },
                    {1, "Maine","ME" },
                    {1, "Montana","MT" },
                    {1, "Nebraska","NE" },
                    {1, "Nevada","NV" },
                    {1, "New Hampshire","NH" },
                    {1, "New Jersey","NJ" },
                    {1, "New Mexico","NM" },
                    {1, "New York","NY" },
                    {1, "North Carolina","NC" },
                    {1, "North Dakota","ND" },
                    {1, "Ohio","OH" },
                    {1, "Oklahoma","OK" },
                    {1, "Oregon","OR" },
                    {1, "Maryland","MD" },
                    {1, "Massachusetts","MA" },
                    {1, "Michigan","MI" },
                    {1, "Minnesota","MN" },
                    {1, "Mississippi","MS" },
                    {1, "Missouri","MO" },
                    {1, "Pennsylvania","PA" },
                    {1, "Rhode Island","RI" },
                    {1, "South Carolina","SC" },
                    {1, "South Dakota","SD" },
                    {1, "Tennessee","TN" },
                    {1, "Texas","TX" },
                    {1, "Utah","UT" },
                    {1, "Vermont","VT" },
                    {1, "Virginia","VA" },
                    {1, "Washington","WA" },
                    {1, "West Virginia","WV" },
                    {1, "Wisconsin","WI" },
                    {1,  "Wyoming","WY" },
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "achievement_step");

            migrationBuilder.DropTable(
                name: "member_achievement");

            migrationBuilder.DropTable(
                name: "member_achievement_progress");

            migrationBuilder.DropTable(
                name: "member_custom_field_value");

            migrationBuilder.DropTable(
                name: "member_renewal");

            migrationBuilder.DropTable(
                name: "achievement");

            migrationBuilder.DropTable(
                name: "custom_field");

            migrationBuilder.DropTable(
                name: "member");

            migrationBuilder.DropTable(
                name: "membership_plan");

            migrationBuilder.DropTable(
                name: "division");

            migrationBuilder.DropTable(
                name: "state_province");

            migrationBuilder.DropTable(
                name: "region");

            migrationBuilder.DropTable(
                name: "country");
        }
    }
}
