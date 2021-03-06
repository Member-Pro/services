using MemberPro.Core.Entities.Media;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MemberPro.Core.Data.Mapping.Media
{
    public class AttachmentMapping : EntityTypeConfiguration<Attachment>
    {
        public override void Configure(EntityTypeBuilder<Attachment> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.OwnerId);

            builder.Property(x => x.ObjectType).IsRequired().HasMaxLength(50);
            builder.Property(x => x.ObjectId);

            builder.Property(x => x.MediaType);

            builder.Property(x => x.SaveFileName).IsRequired().HasMaxLength(255);
            builder.Property(x => x.FileName).IsRequired().HasMaxLength(255);

            builder.Property(x => x.FileSize);
            builder.Property(x => x.ContentType).IsRequired().HasMaxLength(50);

            builder.Property(x => x.Description).HasMaxLength(255);

            builder.Property(x => x.CreatedOn);

            builder.HasOne(x => x.Owner)
                .WithMany()
                .HasForeignKey(x => x.OwnerId);
        }
    }
}
