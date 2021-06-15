using MemberPro.Core.Entities.Achievements;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MemberPro.Core.Data.Mapping.Achievements
{
    public class AchievementRequirementMapping : EntityTypeConfiguration<AchievementRequirement>
    {
        public override void Configure(EntityTypeBuilder<AchievementRequirement> builder)
        {
            // builder.ToTable("AchievementRequirements");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.AchievementId);

            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Description);

            builder.Property(x => x.IsRequired);
            builder.Property(x => x.MinimumCount);

            builder.Property(x => x.IsDisabled);

            builder.Property(x => x.CreatedOn);
            builder.Property(x => x.UpdatedOn);

            builder.HasOne(x => x.Achievement)
                .WithMany(x => x.Requirements)
                .HasForeignKey(x => x.AchievementId)
                .IsRequired();
        }
    }
}
