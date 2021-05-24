using MemberPro.Core.Entities.Achievements;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MemberPro.Core.Data.Mapping.Achievements
{
    public class AchievementMapping : EntityTypeConfiguration<Achievement>
    {
        public override void Configure(EntityTypeBuilder<Achievement> builder)
        {
            builder.ToTable("Achievements");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);

            builder.Property(x => x.Description).HasMaxLength(500);

            builder.Property(x => x.InfoUrl).HasMaxLength(2000);
            builder.Property(x => x.ImageFilename).HasMaxLength(255);

            builder.Property(x => x.IsDisabled);

            builder.Property(x => x.CreatedOn);
            builder.Property(x => x.UpdatedOn);

            builder.HasMany(x => x.Steps)
                .WithOne(x => x.Achievement)
                .HasForeignKey(x => x.AchievementId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
