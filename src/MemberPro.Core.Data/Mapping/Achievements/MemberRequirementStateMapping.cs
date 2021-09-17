using MemberPro.Core.Entities.Achievements;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MemberPro.Core.Data.Mapping.Achievements
{
    public class MemberRequirementStateMapping : EntityTypeConfiguration<MemberRequirementState>
    {
        public override void Configure(EntityTypeBuilder<MemberRequirementState> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.MemberId);
            builder.Property(x => x.RequirementId);

            builder.Property(x => x.UpdatedOn);
            builder.Property(x => x.Data).HasColumnType("jsonb");

            builder.HasOne(x => x.Member)
                .WithMany()
                .HasForeignKey(x => x.MemberId)
                .IsRequired();

            builder.HasOne(x => x.Requirement)
                .WithMany()
                .HasForeignKey(x => x.RequirementId)
                .IsRequired();

            builder.HasQueryFilter(x => x.Requirement.IsDeleted == false);
        }
    }
}
