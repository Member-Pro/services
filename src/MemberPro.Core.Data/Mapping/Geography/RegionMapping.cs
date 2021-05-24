using MemberPro.Core.Entities.Geography;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MemberPro.Core.Data.Mapping.Geography
{
    public class RegionMapping : EntityTypeConfiguration<Region>
    {
        public override void Configure(EntityTypeBuilder<Region> builder)
        {
            builder.ToTable("Regions");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Abbreviation).HasMaxLength(10);

            builder.HasMany(x => x.Divisions)
                .WithOne(x => x.Region)
                .HasForeignKey(x => x.RegionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
