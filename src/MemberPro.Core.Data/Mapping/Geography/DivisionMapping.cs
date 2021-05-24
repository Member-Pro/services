using MemberPro.Core.Entities.Geography;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MemberPro.Core.Data.Mapping.Geography
{
    public class DivisionMapping : EntityTypeConfiguration<Division>
    {
        public override void Configure(EntityTypeBuilder<Division> builder)
        {
            builder.ToTable("Divisions");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.RegionId);

            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Abbreviation).HasMaxLength(10);

            builder.HasOne(x => x.Region)
                .WithMany(x => x.Divisions)
                .HasForeignKey(x => x.RegionId)
                .IsRequired();
        }
    }
}
