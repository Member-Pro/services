﻿// <auto-generated />
using System;
using MemberPro.Core.Data.Implementations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MemberPro.Core.Data.Migrations
{
    [DbContext(typeof(MemberProDbContext))]
    partial class MemberProDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("MemberPro.Core.Entities.Achievements.Achievement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<string>("ImageFilename")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("InfoUrl")
                        .HasMaxLength(2000)
                        .HasColumnType("character varying(2000)");

                    b.Property<bool>("IsDisabled")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("Achievements");
                });

            modelBuilder.Entity("MemberPro.Core.Entities.Achievements.AchievementStep", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("AchievementId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<bool>("IsDisabled")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsRequired")
                        .HasColumnType("boolean");

                    b.Property<int?>("MinimumCount")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("AchievementId");

                    b.ToTable("AchievementSteps");
                });

            modelBuilder.Entity("MemberPro.Core.Entities.Geography.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Abbreviation")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("MemberPro.Core.Entities.Geography.Division", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Abbreviation")
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("RegionId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("RegionId");

                    b.ToTable("Divisions");
                });

            modelBuilder.Entity("MemberPro.Core.Entities.Geography.Region", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Abbreviation")
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.ToTable("Regions");
                });

            modelBuilder.Entity("MemberPro.Core.Entities.Geography.StateProvince", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Abbreviation")
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)");

                    b.Property<int>("CountryId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("StateProvinces");
                });

            modelBuilder.Entity("MemberPro.Core.Entities.Members.CustomField", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<int>("DisplayOrder")
                        .HasColumnType("integer");

                    b.Property<int>("FieldType")
                        .HasColumnType("integer");

                    b.Property<bool>("IsRequired")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("ValueOptions")
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.HasKey("Id");

                    b.ToTable("CustomFields");
                });

            modelBuilder.Entity("MemberPro.Core.Entities.Members.Member", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Address2")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Biography")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("character varying(2000)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<int>("CountryId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("DivisionId")
                        .HasColumnType("integer");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Interests")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<DateTime>("JoinedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<int?>("RegionId")
                        .HasColumnType("integer");

                    b.Property<bool>("ShowInDirectory")
                        .HasColumnType("boolean");

                    b.Property<int>("StateProvinceId")
                        .HasColumnType("integer");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<string>("SubjectId")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.HasIndex("DivisionId");

                    b.HasIndex("RegionId");

                    b.HasIndex("StateProvinceId");

                    b.ToTable("Members");
                });

            modelBuilder.Entity("MemberPro.Core.Entities.Members.MemberAchievement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("AchievementId")
                        .HasColumnType("integer");

                    b.Property<int?>("ApprovedByMemberId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("ApprovedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("DisplayPublicly")
                        .HasColumnType("boolean");

                    b.Property<int>("MemberId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("SubmittedOn")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("AchievementId");

                    b.HasIndex("ApprovedByMemberId");

                    b.HasIndex("MemberId");

                    b.ToTable("MemberAchievements");
                });

            modelBuilder.Entity("MemberPro.Core.Entities.Members.MemberAchievementProgress", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("AchievementId")
                        .HasColumnType("integer");

                    b.Property<int>("Amount")
                        .HasColumnType("integer");

                    b.Property<string>("Comments")
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("MemberId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AchievementId");

                    b.HasIndex("MemberId");

                    b.ToTable("MemberAchievementProgress");
                });

            modelBuilder.Entity("MemberPro.Core.Entities.Members.MemberCustomFieldValue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("FieldId")
                        .HasColumnType("integer");

                    b.Property<int>("MemberId")
                        .HasColumnType("integer");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("FieldId");

                    b.HasIndex("MemberId");

                    b.ToTable("MemberCustomFieldValues");
                });

            modelBuilder.Entity("MemberPro.Core.Entities.Members.MemberRenewal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Comments")
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("MemberId")
                        .HasColumnType("integer");

                    b.Property<decimal?>("PaidAmount")
                        .HasColumnType("numeric");

                    b.Property<DateTime?>("PaidDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("PlanId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("TransactionId")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.HasIndex("MemberId");

                    b.HasIndex("PlanId");

                    b.ToTable("MemberRenewals");
                });

            modelBuilder.Entity("MemberPro.Core.Entities.Plans.MembershipPlan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime?>("AvailableEndDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("AvailableStartDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<int>("DurationInMonths")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric(8,2)");

                    b.Property<string>("SKU")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("MembershipPlans");
                });

            modelBuilder.Entity("MemberPro.Core.Entities.Achievements.AchievementStep", b =>
                {
                    b.HasOne("MemberPro.Core.Entities.Achievements.Achievement", "Achievement")
                        .WithMany("Steps")
                        .HasForeignKey("AchievementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Achievement");
                });

            modelBuilder.Entity("MemberPro.Core.Entities.Geography.Division", b =>
                {
                    b.HasOne("MemberPro.Core.Entities.Geography.Region", "Region")
                        .WithMany("Divisions")
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Region");
                });

            modelBuilder.Entity("MemberPro.Core.Entities.Geography.StateProvince", b =>
                {
                    b.HasOne("MemberPro.Core.Entities.Geography.Country", "Country")
                        .WithMany("StateProvinces")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("MemberPro.Core.Entities.Members.Member", b =>
                {
                    b.HasOne("MemberPro.Core.Entities.Geography.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MemberPro.Core.Entities.Geography.Division", "Division")
                        .WithMany()
                        .HasForeignKey("DivisionId");

                    b.HasOne("MemberPro.Core.Entities.Geography.Region", "Region")
                        .WithMany()
                        .HasForeignKey("RegionId");

                    b.HasOne("MemberPro.Core.Entities.Geography.StateProvince", "StateProvince")
                        .WithMany()
                        .HasForeignKey("StateProvinceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");

                    b.Navigation("Division");

                    b.Navigation("Region");

                    b.Navigation("StateProvince");
                });

            modelBuilder.Entity("MemberPro.Core.Entities.Members.MemberAchievement", b =>
                {
                    b.HasOne("MemberPro.Core.Entities.Achievements.Achievement", "Achievement")
                        .WithMany()
                        .HasForeignKey("AchievementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MemberPro.Core.Entities.Members.Member", "ApprovedByMember")
                        .WithMany()
                        .HasForeignKey("ApprovedByMemberId");

                    b.HasOne("MemberPro.Core.Entities.Members.Member", "Member")
                        .WithMany("Achievements")
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Achievement");

                    b.Navigation("ApprovedByMember");

                    b.Navigation("Member");
                });

            modelBuilder.Entity("MemberPro.Core.Entities.Members.MemberAchievementProgress", b =>
                {
                    b.HasOne("MemberPro.Core.Entities.Achievements.Achievement", "Achievement")
                        .WithMany()
                        .HasForeignKey("AchievementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MemberPro.Core.Entities.Members.Member", "Member")
                        .WithMany("AchievementProgressRecords")
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Achievement");

                    b.Navigation("Member");
                });

            modelBuilder.Entity("MemberPro.Core.Entities.Members.MemberCustomFieldValue", b =>
                {
                    b.HasOne("MemberPro.Core.Entities.Members.CustomField", "Field")
                        .WithMany()
                        .HasForeignKey("FieldId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MemberPro.Core.Entities.Members.Member", "Member")
                        .WithMany("FieldValues")
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Field");

                    b.Navigation("Member");
                });

            modelBuilder.Entity("MemberPro.Core.Entities.Members.MemberRenewal", b =>
                {
                    b.HasOne("MemberPro.Core.Entities.Members.Member", "Member")
                        .WithMany("Renewals")
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MemberPro.Core.Entities.Plans.MembershipPlan", "Plan")
                        .WithMany()
                        .HasForeignKey("PlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Member");

                    b.Navigation("Plan");
                });

            modelBuilder.Entity("MemberPro.Core.Entities.Achievements.Achievement", b =>
                {
                    b.Navigation("Steps");
                });

            modelBuilder.Entity("MemberPro.Core.Entities.Geography.Country", b =>
                {
                    b.Navigation("StateProvinces");
                });

            modelBuilder.Entity("MemberPro.Core.Entities.Geography.Region", b =>
                {
                    b.Navigation("Divisions");
                });

            modelBuilder.Entity("MemberPro.Core.Entities.Members.Member", b =>
                {
                    b.Navigation("AchievementProgressRecords");

                    b.Navigation("Achievements");

                    b.Navigation("FieldValues");

                    b.Navigation("Renewals");
                });
#pragma warning restore 612, 618
        }
    }
}
