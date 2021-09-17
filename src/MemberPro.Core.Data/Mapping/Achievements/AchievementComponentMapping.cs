using MemberPro.Core.Entities.Achievements;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MemberPro.Core.Data.Mapping.Achievements
{
    public class AchievementComponentMapping : EntityTypeConfiguration<AchievementComponent>
    {
        public override void Configure(EntityTypeBuilder<AchievementComponent> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Description);

            builder.Property(x => x.IsDisabled);

            builder.Property(x => x.CreatedOn);
            builder.Property(x => x.UpdatedOn);

            builder.Property(x => x.IsDeleted);
            builder.HasQueryFilter(x => x.IsDeleted == false);

            builder.HasMany(x => x.Requirements)
                .WithOne(x => x.Component)
                .IsRequired()
                .HasForeignKey(x => x.ComponentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
