using MemberPro.Core.Entities.Members;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MemberPro.Core.Data.Mapping.Members
{
    public class MemberCustomFieldValueMapping : EntityTypeConfiguration<MemberCustomFieldValue>
    {
        public override void Configure(EntityTypeBuilder<MemberCustomFieldValue> builder)
        {
            builder.ToTable("MemberCustomFieldValues");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.MemberId);
            builder.Property(x => x.FieldId);
            builder.Property(x => x.Value);

            builder.HasOne(x => x.Member)
                .WithMany(x => x.FieldValues)
                .IsRequired()
                .HasForeignKey(x => x.MemberId);

            builder.HasOne(x => x.Field)
                .WithMany()
                .IsRequired()
                .HasForeignKey(x => x.FieldId);
        }
    }


}
