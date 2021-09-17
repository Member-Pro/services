using MemberPro.Core.Entities.Achievements;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MemberPro.Core.Data.Mapping.Achievements
{
    public class RequirementMapping : EntityTypeConfiguration<Requirement>
    {
        public override void Configure(EntityTypeBuilder<Requirement> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.ComponentId);

            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Description);

            builder.Property(x => x.ValidatorTypeName).HasMaxLength(255);
            builder.Property(x => x.ValidationParameters).HasColumnType("jsonb");

            builder.Property(x => x.Type);

            builder.Property(x => x.MinCount);
            builder.Property(x => x.MaxCount);

            builder.Property(x => x.IsDeleted);
            builder.HasQueryFilter(x => x.IsDeleted == false);

            builder.HasOne(x => x.Component)
                .WithMany(x => x.Requirements)
                .HasForeignKey(x => x.ComponentId)
                .IsRequired();
        }
    }
}
