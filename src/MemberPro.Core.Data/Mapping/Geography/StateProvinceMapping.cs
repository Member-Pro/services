using MemberPro.Core.Entities.Geography;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MemberPro.Core.Data.Mapping.Geography
{
    public class StateProvinceMapping : EntityTypeConfiguration<StateProvince>
    {
        public override void Configure(EntityTypeBuilder<StateProvince> builder)
        {
            builder.ToTable("StateProvinces");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CountryId);

            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Abbreviation).HasMaxLength(10);

            builder.HasOne(x => x.Country)
                .WithMany(x => x.StateProvinces)
                .HasForeignKey(x => x.CountryId)
                .IsRequired();
        }
    }
}
