using MemberPro.Core.Entities.Organizations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MemberPro.Core.Data.Mapping.Organizations
{
    public class OfficerPositionMapping : EntityTypeConfiguration<OfficerPosition>
    {
        public override void Configure(EntityTypeBuilder<OfficerPosition> builder)
        {
            // table name is officer_position per pgsql

            builder.HasKey(x => x.Id);

            builder.Property(x => x.OrganizationId);

            builder.Property(x => x.Title).HasMaxLength(50).IsRequired();
            builder.Property(x => x.PositionType);

            builder.HasOne(x => x.Organization)
                .WithMany()
                .IsRequired()
                .HasForeignKey(x => x.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
