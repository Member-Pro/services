using MemberPro.Core.Entities.Members;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MemberPro.Core.Data.Mapping.Members
{
    public class MemberRenewalMapping : EntityTypeConfiguration<MemberRenewal>
    {
        public override void Configure(EntityTypeBuilder<MemberRenewal> builder)
        {
            builder.ToTable("MemberRenewals");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.MemberId);
            builder.Property(x => x.PlanId);

            builder.Property(x => x.StartDate);
            builder.Property(x => x.EndDate);

            builder.Property(x => x.PaidDate);
            builder.Property(x => x.PaidAmount);
            builder.Property(x => x.TransactionId).HasMaxLength(50);

            builder.Property(x => x.Comments).HasMaxLength(1000);

            builder.HasOne(x => x.Member)
                .WithMany(x => x.Renewals)
                .IsRequired()
                .HasForeignKey(x => x.MemberId);

            builder.HasOne(x => x.Plan)
                .WithMany()
                .IsRequired()
                .HasForeignKey(x => x.PlanId);
        }
    }


}
