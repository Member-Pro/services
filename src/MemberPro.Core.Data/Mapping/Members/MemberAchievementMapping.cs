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
            builder.Property(x => x.SubmittedOn);
            builder.Property(x => x.ApprovedOn);
            builder.Property(x => x.ApprovedByMemberId);
            builder.Property(x => x.DisplayPublicly);

            builder.HasOne(x => x.Member)
                .WithMany(x => x.Achievements)
                .IsRequired()
                .HasForeignKey(x => x.MemberId);

            builder.HasOne(x => x.Achievement)
                .WithMany()
                .IsRequired()
                .HasForeignKey(x => x.AchievementId);

            builder.HasOne(x => x.ApprovedByMember)
                .WithMany()
                .HasForeignKey(x => x.ApprovedByMemberId);
        }
    }


}
