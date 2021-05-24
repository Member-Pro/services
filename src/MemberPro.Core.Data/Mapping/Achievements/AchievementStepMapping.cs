using MemberPro.Core.Entities.Achievements;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MemberPro.Core.Data.Mapping.Achievements
{
    public class AchievementStepMapping : EntityTypeConfiguration<AchievementStep>
    {
        public override void Configure(EntityTypeBuilder<AchievementStep> builder)
        {
            builder.ToTable("AchievementSteps");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.AchievementId);

            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Description).HasMaxLength(500);

            builder.Property(x => x.IsRequired);
            builder.Property(x => x.MinimumCount);

            builder.Property(x => x.IsDisabled);

            builder.Property(x => x.CreatedOn);
            builder.Property(x => x.UpdatedOn);

            builder.HasOne(x => x.Achievement)
                .WithMany(x => x.Steps)
                .HasForeignKey(x => x.AchievementId)
                .IsRequired();
        }
    }
}
