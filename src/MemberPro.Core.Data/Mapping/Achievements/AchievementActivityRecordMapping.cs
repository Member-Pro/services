using MemberPro.Core.Entities.Achievements;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace MemberPro.Core.Data.Mapping.Achievements
{
    public class AchievementActivityMapping : EntityTypeConfiguration<AchievementActivity>
    {
        public override void Configure(EntityTypeBuilder<AchievementActivity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.AchievementId);
            builder.Property(x => x.ComponentId);
            builder.Property(x => x.MemberId);

            builder.Property(x => x.ActivityDate);
            builder.Property(x => x.Description).HasMaxLength(500);

            builder.Property(x => x.QuantityCompleted);

            builder.Property(x => x.Comments).HasMaxLength(500);

            builder.HasOne(x => x.Achievement)
                .WithMany()
                .IsRequired()
                .HasForeignKey(x => x.AchievementId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Component)
                .WithMany()
                .HasForeignKey(x => x.ComponentId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(x => x.Member)
                .WithMany()
                .IsRequired()
                .HasForeignKey(x => x.MemberId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
