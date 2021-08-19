using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.SimpleNotificationService;
using MemberPro.Core.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MemberPro.Core.Services.Media
{
    public class AmazonS3StorageService : IFileStorageService
    {
        public AmazonS3StorageService(
            IAmazonS3 amazonS3,
            IMediaHelper mediaHelper,
            IAmazonSimpleNotificationService simpleNotificationService,
            IOptions<FileStorageConfig> fileStoreConfig,
            ILogger<AmazonS3StorageService> logger)
        {
            _amazonS3 = amazonS3;
            _mediaHelper = mediaHelper;
            _simpleNotificationService = simpleNotificationService;
            _fileStoreConfig = fileStoreConfig.Value;
        }

        const int objectMaxAge = 2592000;

        private readonly IAmazonS3 _amazonS3;
        private readonly IMediaHelper _mediaHelper;
        private readonly IAmazonSimpleNotificationService _simpleNotificationService;
        private readonly FileStorageConfig _fileStoreConfig;

        public async Task<bool> FileExistsAsync(string path)
        {
            try
            {
                var response = await _amazonS3.GetObjectMetadataAsync(_fileStoreConfig.S3.BucketName, path);
                return true; // If this succeeds then it was found
            }
            catch(AmazonS3Exception ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return false;
                }

                throw;
            }
        }

        public string ResolveFilePath(string path) => throw new NotImplementedException();

        public string ResolveFileUrl(string path)
        {
            if (path.StartsWith("/"))
            {
                path = path.Substring(1);
            }

            return $"https://s3.amazonaws.com/{_fileStoreConfig.S3.BucketName}/{path}";
        }

        public async Task SaveFileAsync(string path, string contentType, Stream stream)
        {
            var putRequest = new PutObjectRequest
            {
                BucketName = _fileStoreConfig.S3.BucketName,
                InputStream = stream,
                Key = path,
                AutoCloseStream = false,
                ContentType = contentType,
                CannedACL = S3CannedACL.PublicRead
            };

            putRequest.Headers.CacheControl = $"max-age={objectMaxAge}";

            await _amazonS3.PutObjectAsync(putRequest);

            // If this is an image file and there is an ImageResizer SNS topic ID, send message for resizing
            if (_mediaHelper.IsImageContentType(contentType)
                && !string.IsNullOrEmpty(_fileStoreConfig.S3.ImageResizerTopicArn))
            {
                // TODO: Serialize required data
                // - Bucket Name, Object key, size key, size width, size height

                var snsMessageData = new
                {
                    S3Object = new
                    {
                        bucketName = _fileStoreConfig.S3.BucketName,
                        key = path,
                    }
                };

                var snsMessage = JsonSerializer.Serialize(snsMessageData);

                await _simpleNotificationService.PublishAsync(_fileStoreConfig.S3.ImageResizerTopicArn, snsMessage);
            }
        }

        public async Task DeleteFileAsync(string path)
        {
            var deleteRequest = new DeleteObjectRequest
            {
                BucketName = _fileStoreConfig.S3.BucketName,
                Key = path
            };

            await _amazonS3.DeleteObjectAsync(deleteRequest);
        }
    }
}
