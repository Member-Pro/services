using MemberPro.Core.Entities.Members;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MemberPro.Core.Data.Mapping.Members
{
    public class MemberMapping : EntityTypeConfiguration<Member>
    {
        public override void Configure(EntityTypeBuilder<Member> builder)
        {
            // builder.ToTable("Members");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.SubjectId).HasMaxLength(50);
            builder.Property(x => x.FirstName).HasMaxLength(50).IsRequired();
            builder.Property(x => x.LastName).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Status);
            builder.Property(x => x.JoinedOn);
            builder.Property(x => x.EmailAddress).HasMaxLength(255).IsRequired();
            builder.Property(x => x.DateOfBirth);
            builder.Property(x => x.CountryId);
            builder.Property(x => x.StateProvinceId);
            builder.Property(x => x.Address).HasMaxLength(50);
            builder.Property(x => x.Address2).HasMaxLength(50);
            builder.Property(x => x.City).HasMaxLength(50);
            builder.Property(x => x.PostalCode).HasMaxLength(20);
            builder.Property(x => x.PhoneNumber).HasMaxLength(20);
            builder.Property(x => x.ShowInDirectory);
            builder.Property(x => x.OrganizationId);
            builder.Property(x => x.Biography).HasMaxLength(2000);
            builder.Property(x => x.Interests).HasMaxLength(1000);

            builder.HasOne(x => x.Country)
                .WithMany()
                .HasForeignKey(x => x.CountryId);

            builder.HasOne(x => x.StateProvince)
                .WithMany()
                .HasForeignKey(x => x.StateProvinceId);

            builder.HasOne(x => x.Organization)
                .WithMany()
                .HasForeignKey(x => x.OrganizationId);

            builder.HasMany(x => x.FieldValues)
                .WithOne(x => x.Member)
                .HasForeignKey(x => x.MemberId);

            builder.HasMany(x => x.Renewals)
                .WithOne(x => x.Member)
                .HasForeignKey(x => x.MemberId);

            builder.HasMany(x => x.Achievements)
                .WithOne(x => x.Member)
                .HasForeignKey(x => x.MemberId);

            builder.HasMany(x => x.AchievementProgressRecords)
                .WithOne(x => x.Member)
                .HasForeignKey(x => x.MemberId);
        }
    }


}
