namespace MemberPro.Core.Configuration
{
    public class FileStorageConfig
    {
        public FileStorageProvider Provider { get; set; }

        public AmazonS3StorageConfig S3 { get; set; }
    }

    public enum FileStorageProvider
    {
        AWS_S3,
    }
}
