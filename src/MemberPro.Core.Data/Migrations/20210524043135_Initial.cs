using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MemberPro.Core.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Achievements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    InfoUrl = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    ImageFilename = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    IsDisabled = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Achievements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Abbreviation = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomFields",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    DisplayOrder = table.Column<int>(type: "integer", nullable: false),
                    IsRequired = table.Column<bool>(type: "boolean", nullable: false),
                    FieldType = table.Column<int>(type: "integer", nullable: false),
                    ValueOptions = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomFields", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MembershipPlans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    SKU = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    AvailableStartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    AvailableEndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Price = table.Column<decimal>(type: "numeric(8,2)", nullable: false),
                    DurationInMonths = table.Column<int>(type: "integer", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MembershipPlans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Abbreviation = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AchievementSteps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AchievementId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    IsRequired = table.Column<bool>(type: "boolean", nullable: false),
                    MinimumCount = table.Column<int>(type: "integer", nullable: true),
                    IsDisabled = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AchievementSteps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AchievementSteps_Achievements_AchievementId",
                        column: x => x.AchievementId,
                        principalTable: "Achievements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StateProvinces",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CountryId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Abbreviation = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StateProvinces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StateProvinces_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Divisions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RegionId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Abbreviation = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Divisions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Divisions_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SubjectId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    FirstName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    JoinedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EmailAddress = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CountryId = table.Column<int>(type: "integer", nullable: false),
                    StateProvinceId = table.Column<int>(type: "integer", nullable: false),
                    Address = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Address2 = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    City = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    PostalCode = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    PhoneNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    ShowInDirectory = table.Column<bool>(type: "boolean", nullable: false),
                    RegionId = table.Column<int>(type: "integer", nullable: true),
                    DivisionId = table.Column<int>(type: "integer", nullable: true),
                    Biography = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    Interests = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Members_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Members_Divisions_DivisionId",
                        column: x => x.DivisionId,
                        principalTable: "Divisions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Members_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Members_StateProvinces_StateProvinceId",
                        column: x => x.StateProvinceId,
                        principalTable: "StateProvinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MemberAchievementProgress",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MemberId = table.Column<int>(type: "integer", nullable: false),
                    AchievementId = table.Column<int>(type: "integer", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Amount = table.Column<int>(type: "integer", nullable: false),
                    Comments = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberAchievementProgress", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MemberAchievementProgress_Achievements_AchievementId",
                        column: x => x.AchievementId,
                        principalTable: "Achievements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MemberAchievementProgress_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MemberAchievements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MemberId = table.Column<int>(type: "integer", nullable: false),
                    AchievementId = table.Column<int>(type: "integer", nullable: false),
                    SubmittedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ApprovedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ApprovedByMemberId = table.Column<int>(type: "integer", nullable: true),
                    DisplayPublicly = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberAchievements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MemberAchievements_Achievements_AchievementId",
                        column: x => x.AchievementId,
                        principalTable: "Achievements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MemberAchievements_Members_ApprovedByMemberId",
                        column: x => x.ApprovedByMemberId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MemberAchievements_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MemberCustomFieldValues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MemberId = table.Column<int>(type: "integer", nullable: false),
                    FieldId = table.Column<int>(type: "integer", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberCustomFieldValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MemberCustomFieldValues_CustomFields_FieldId",
                        column: x => x.FieldId,
                        principalTable: "CustomFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MemberCustomFieldValues_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MemberRenewals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MemberId = table.Column<int>(type: "integer", nullable: false),
                    PlanId = table.Column<int>(type: "integer", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    PaidDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    PaidAmount = table.Column<decimal>(type: "numeric", nullable: true),
                    TransactionId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Comments = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberRenewals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MemberRenewals_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MemberRenewals_MembershipPlans_PlanId",
                        column: x => x.PlanId,
                        principalTable: "MembershipPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AchievementSteps_AchievementId",
                table: "AchievementSteps",
                column: "AchievementId");

            migrationBuilder.CreateIndex(
                name: "IX_Divisions_RegionId",
                table: "Divisions",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_MemberAchievementProgress_AchievementId",
                table: "MemberAchievementProgress",
                column: "AchievementId");

            migrationBuilder.CreateIndex(
                name: "IX_MemberAchievementProgress_MemberId",
                table: "MemberAchievementProgress",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_MemberAchievements_AchievementId",
                table: "MemberAchievements",
                column: "AchievementId");

            migrationBuilder.CreateIndex(
                name: "IX_MemberAchievements_ApprovedByMemberId",
                table: "MemberAchievements",
                column: "ApprovedByMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_MemberAchievements_MemberId",
                table: "MemberAchievements",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_MemberCustomFieldValues_FieldId",
                table: "MemberCustomFieldValues",
                column: "FieldId");

            migrationBuilder.CreateIndex(
                name: "IX_MemberCustomFieldValues_MemberId",
                table: "MemberCustomFieldValues",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_MemberRenewals_MemberId",
                table: "MemberRenewals",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_MemberRenewals_PlanId",
                table: "MemberRenewals",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Members_CountryId",
                table: "Members",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Members_DivisionId",
                table: "Members",
                column: "DivisionId");

            migrationBuilder.CreateIndex(
                name: "IX_Members_RegionId",
                table: "Members",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Members_StateProvinceId",
                table: "Members",
                column: "StateProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_StateProvinces_CountryId",
                table: "StateProvinces",
                column: "CountryId");

            // SEED DATA

            migrationBuilder.InsertData("Countries",
                columns: new[] { "Id", "Name", "Abbreviation" },
                values: new object[,]
                {
                    { 1, "United States", "US" },
                    { 2, "Canada", "CA" }
                });

            migrationBuilder.InsertData("StateProvinces",
                columns: new[] { "CountryId", "Name", "Abbreviation" },
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
                name: "AchievementSteps");

            migrationBuilder.DropTable(
                name: "MemberAchievementProgress");

            migrationBuilder.DropTable(
                name: "MemberAchievements");

            migrationBuilder.DropTable(
                name: "MemberCustomFieldValues");

            migrationBuilder.DropTable(
                name: "MemberRenewals");

            migrationBuilder.DropTable(
                name: "Achievements");

            migrationBuilder.DropTable(
                name: "CustomFields");

            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.DropTable(
                name: "MembershipPlans");

            migrationBuilder.DropTable(
                name: "Divisions");

            migrationBuilder.DropTable(
                name: "StateProvinces");

            migrationBuilder.DropTable(
                name: "Regions");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
