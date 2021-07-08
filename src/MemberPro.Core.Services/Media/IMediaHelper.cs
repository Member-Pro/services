using MemberPro.Core.Entities.Media;

namespace MemberPro.Core.Services.Media
{
    public interface IMediaHelper
    {
        AttachmentType GetMediaTypeFromContentType(string contentType);
    }

    public class MediaHelper : IMediaHelper
    {
        public AttachmentType GetMediaTypeFromContentType(string contentType)
        {
            switch (contentType)
            {
                case "image/gif":
                case "image/jpeg":
                case "image/png":
                    return AttachmentType.Photo;
                case "application/pdf":
                    return AttachmentType.Pdf;
                case "application/msword":
                case "application/vnd.openxmlformats-officedocument.wordprocessingml.document":
                    return AttachmentType.WordDocument;
                case "application/vnd.ms-excel":
                case "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet":
                    return AttachmentType.ExcelDocument;
                default:
                    return AttachmentType.OtherDocument;
            }
        }
    }
}