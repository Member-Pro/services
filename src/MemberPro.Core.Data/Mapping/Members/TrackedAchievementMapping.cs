using MemberPro.Core.Entities.Members;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace MemberPro.Core.Data.Mapping.Members
{
    public class TrackedAchievementMapping : EntityTypeConfiguration<TrackedAchievement>
    {
        public override void Configure(EntityTypeBuilder<TrackedAchievement> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.MemberId);

            builder.Property(x => x.AchievementId);

            builder.Property(x => x.Notes).HasMaxLength(1000);

            builder.Property(x => x.CreatedOn);

            builder.HasOne(x => x.Member)
                .WithMany()
                .HasForeignKey(x => x.MemberId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Achievement)
                .WithMany()
                .HasForeignKey(x => x.AchievementId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
