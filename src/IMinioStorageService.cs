namespace MinioStorage
{
    public interface IMinioStorageService
    {
        Task<bool> BucketExistsAsync(string bucketName, CancellationToken cancellationToken = default);
        Task CreateBucketAsync(string bucketName, CancellationToken cancellationToken = default);
        Task<string> GetPresignedUrlAsync(string bucketName, string objectName, int expiryInSeconds = 3600, CancellationToken cancellationToken = default);
        Task UploadObjectAsync(string bucketName, string objectName, Stream data, string contentType, CancellationToken cancellationToken = default);
        Task<Stream> GetObjectAsync(string bucketName, string objectName, CancellationToken cancellationToken = default);
        Task RemoveObjectAsync(string bucketName, string objectName, CancellationToken cancellationToken = default);
    }
}