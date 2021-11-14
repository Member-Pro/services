using MemberPro.Core.Entities.Organizations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace MemberPro.Core.Data.Mapping.Organizations
{
    public class OrganizationMapping : EntityTypeConfiguration<Organization>
    {
        public override void Configure(EntityTypeBuilder<Organization> builder)
        {
            // table name is organization per pgsql

            builder.HasKey(x => x.Id);

            builder.Property(x => x.ParentId);

            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Abbreviation).IsRequired().HasMaxLength(10);

            builder.Property(x => x.Description).HasMaxLength(500);

            builder.Property(x => x.CreatedOn);
            builder.Property(x => x.UpdatedOn);

            builder.HasOne(x => x.Parent)
                .WithMany(x => x.Children)
                .HasForeignKey(x => x.ParentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Children)
                .WithOne(x => x.Parent)
                .HasForeignKey(x => x.ParentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
