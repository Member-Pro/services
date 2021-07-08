using MemberPro.Core.Entities.Media;

namespace MemberPro.Core.Services.Media
{
    public interface IMediaHelper
    {
        AttachmentMediaType GetMediaTypeFromContentType(string contentType);
    }

    public class MediaHelper : IMediaHelper
    {
        public AttachmentMediaType GetMediaTypeFromContentType(string contentType)
        {
            switch (contentType)
            {
                case "image/gif":
                case "image/jpeg":
                case "image/png":
                    return AttachmentMediaType.Photo;
                case "application/pdf":
                    return AttachmentMediaType.Pdf;
                case "application/msword":
                case "application/vnd.openxmlformats-officedocument.wordprocessingml.document":
                    return AttachmentMediaType.WordDocument;
                case "application/vnd.ms-excel":
                case "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet":
                    return AttachmentMediaType.ExcelDocument;
                default:
                    return AttachmentMediaType.OtherDocument;
            }
        }
    }
}
