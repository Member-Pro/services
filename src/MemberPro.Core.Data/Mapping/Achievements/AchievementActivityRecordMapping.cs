using MemberPro.Core.Entities.Achievements;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace MemberPro.Core.Data.Mapping.Achievements
{
    public class AchievementActivityRecordMapping : EntityTypeConfiguration<AchievementActivityRecord>
    {
        public override void Configure(EntityTypeBuilder<AchievementActivityRecord> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.AchievementId);
            builder.Property(x => x.RequirementId);
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

            builder.HasOne(x => x.Requirement)
                .WithMany()
                .IsRequired()
                .HasForeignKey(x => x.RequirementId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Member)
                .WithMany()
                .IsRequired()
                .HasForeignKey(x => x.MemberId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
