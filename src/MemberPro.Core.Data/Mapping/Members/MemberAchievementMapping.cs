using MemberPro.Core.Entities.Members;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MemberPro.Core.Data.Mapping.Members
{
    public class MemberAchievementMapping : EntityTypeConfiguration<MemberAchievement>
    {
        public override void Configure(EntityTypeBuilder<MemberAchievement> builder)
        {
            // builder.ToTable("MemberAchievements");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.MemberId);
            builder.Property(x => x.AchievementId);
            builder.Property(x => x.EarnedOn);
            builder.Property(x => x.DisplayPublicly);
            builder.Property(x => x.CreatedOn);
            builder.Property(x => x.CreatedByMemberId);

            builder.HasOne(x => x.Member)
                .WithMany(x => x.Achievements)
                .IsRequired()
                .HasForeignKey(x => x.MemberId);

            builder.HasOne(x => x.Achievement)
                .WithMany()
                .IsRequired()
                .HasForeignKey(x => x.AchievementId);

            builder.HasOne(x => x.CreatedByMember)
                .WithMany()
                .IsRequired()
                .HasForeignKey(x => x.CreatedByMemberId);

            builder.HasQueryFilter(x => x.Achievement.IsDeleted == false);
        }
    }


}
