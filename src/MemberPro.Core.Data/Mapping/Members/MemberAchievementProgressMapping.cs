using MemberPro.Core.Entities.Members;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MemberPro.Core.Data.Mapping.Members
{
    public class MemberAchievementProgressMapping : EntityTypeConfiguration<MemberAchievementProgress>
    {
        public override void Configure(EntityTypeBuilder<MemberAchievementProgress> builder)
        {
            // builder.ToTable("MemberAchievementProgress");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.MemberId);
            builder.Property(x => x.AchievementId);
            builder.Property(x => x.CreatedOn);
            builder.Property(x => x.Amount);
            builder.Property(x => x.Comments).HasMaxLength(1000);

            builder.HasOne(x => x.Member)
                .WithMany(x => x.AchievementProgressRecords)
                .IsRequired()
                .HasForeignKey(x => x.MemberId);

            builder.HasOne(x => x.Achievement)
                .WithMany()
                .IsRequired()
                .HasForeignKey(x => x.AchievementId);

            builder.HasQueryFilter(x => x.Achievement.IsDeleted == false);
        }
    }


}
