using MemberPro.Core.Entities.Organizations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace MemberPro.Core.Data.Mapping.Organizations
{
    public class OfficerMapping : EntityTypeConfiguration<Officer>
    {
        public override void Configure(EntityTypeBuilder<Officer> builder)
        {
            // table name is officer per pgsql

            builder.HasKey(x => x.Id);

            builder.Property(x => x.PositionId);
            builder.Property(x => x.MemberId);

            builder.Property(x => x.TermStart);
            builder.Property(x => x.TermEnd);

            builder.HasOne(x => x.Position)
                .WithMany()
                .IsRequired()
                .HasForeignKey(x => x.PositionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Member)
                .WithMany()
                .IsRequired()
                .HasForeignKey(x => x.MemberId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
