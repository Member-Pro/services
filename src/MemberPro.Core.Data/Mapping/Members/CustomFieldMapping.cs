using MemberPro.Core.Entities.Members;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MemberPro.Core.Data.Mapping.Members
{
    public class CustomFieldMapping : EntityTypeConfiguration<CustomField>
    {
        public override void Configure(EntityTypeBuilder<CustomField> builder)
        {
            // builder.ToTable("CustomFields");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(500);

            builder.Property(x => x.DisplayOrder);

            builder.Property(x => x.IsRequired);

            builder.Property(x => x.FieldType);
            builder.Property(x => x.ValueOptions).HasMaxLength(1000);
        }
    }
}
