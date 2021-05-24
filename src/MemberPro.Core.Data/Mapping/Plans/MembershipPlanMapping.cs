using MemberPro.Core.Entities.Plans;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MemberPro.Core.Data.Mapping.Plans
{
    public class MembershipPlanMapping : EntityTypeConfiguration<MembershipPlan>
    {
        public override void Configure(EntityTypeBuilder<MembershipPlan> builder)
        {
            builder.ToTable("MembershipPlans");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.SKU).HasMaxLength(50);

            builder.Property(x => x.Description).HasMaxLength(500);

            builder.Property(x => x.AvailableStartDate);
            builder.Property(x => x.AvailableEndDate);

            builder.Property(x => x.Price).HasColumnType("decimal(8,2)");

            builder.Property(x => x.DurationInMonths);

            builder.Property(x => x.CreatedOn);
            builder.Property(x => x.UpdatedOn);
        }
    }
}
