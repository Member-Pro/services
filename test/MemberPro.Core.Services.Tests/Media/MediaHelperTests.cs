using Xunit;
using MemberPro.Core.Services.Media;
using MemberPro.Core.Entities.Media;

namespace MemberPro.Core.Services.Tests.Media
{
    public class MediaHelperTests
    {
        [Theory]
        [InlineData("image/gif")]
        [InlineData("image/jpeg")]
        [InlineData("image/png")]
        public void GetMediaTypeFromContentType_ReturnsPhoto_ForImageContentTypes(string contentType)
        {
            // Arrange
            var mediaHelper = new MediaHelper();

            // Act
            var mediaType = mediaHelper.GetMediaTypeFromContentType(contentType);

            // Assert
            Assert.Equal(AttachmentMediaType.Photo, mediaType);
        }

        [Theory]
        [InlineData("application/pdf")]
        public void GetMediaTypeFromContentType_ReturnsPdf_ForPdfContentTypes(string contentType)
        {
            // Arrange
            var mediaHelper = new MediaHelper();

            // Act
            var mediaType = mediaHelper.GetMediaTypeFromContentType(contentType);

            // Assert
            Assert.Equal(AttachmentMediaType.Pdf, mediaType);
        }

        [Theory]
        [InlineData("application/msword")]
        [InlineData("application/vnd.openxmlformats-officedocument.wordprocessingml.document")]
        public void GetMediaTypeFromContentType_ReturnsWordDoc_ForWordDocContentTypes(string contentType)
        {
            // Arrange
            var mediaHelper = new MediaHelper();

            // Act
            var mediaType = mediaHelper.GetMediaTypeFromContentType(contentType);

            // Assert
            Assert.Equal(AttachmentMediaType.WordDocument, mediaType);
        }

        [Theory]
        [InlineData("application/vnd.ms-excel")]
        [InlineData("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")]
        public void GetMediaTypeFromContentType_ReturnsExcel_ForExcelDocContentTypes(string contentType)
        {
            // Arrange
            var mediaHelper = new MediaHelper();

            // Act
            var mediaType = mediaHelper.GetMediaTypeFromContentType(contentType);

            // Assert
            Assert.Equal(AttachmentMediaType.ExcelDocument, mediaType);
        }
    }
}
