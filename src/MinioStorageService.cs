using Minio;
using Minio.DataModel.Args;

namespace MinioStorage
{
    public class MinioStorageService : IMinioStorageService
    {
        private readonly IMinioClient _minioClient;

        public MinioStorageService(IMinioClient minioClient)
        {
            _minioClient = minioClient ?? throw new ArgumentNullException(nameof(minioClient));
        }

        public async Task<bool> BucketExistsAsync(string bucketName, CancellationToken cancellationToken = default)
        {
            var args = new BucketExistsArgs().WithBucket(bucketName);
            return await _minioClient.BucketExistsAsync(args, cancellationToken).ConfigureAwait(false);
        }

        public async Task CreateBucketAsync(string bucketName, CancellationToken cancellationToken = default)
        {
            var args = new MakeBucketArgs().WithBucket(bucketName);
            await _minioClient.MakeBucketAsync(args, cancellationToken).ConfigureAwait(false);
        }

        public async Task<string> GetPresignedUrlAsync(string bucketName, string objectName, int expiryInSeconds = 3600, CancellationToken cancellationToken = default)
        {
            var args = new PresignedGetObjectArgs()
                .WithBucket(bucketName)
                .WithObject(objectName)
                .WithExpiry(expiryInSeconds);

            return await _minioClient.PresignedGetObjectAsync(args).ConfigureAwait(false);
        }

        public async Task UploadObjectAsync(string bucketName, string objectName, Stream data, string contentType, CancellationToken cancellationToken = default)
        {
            var args = new PutObjectArgs()
                .WithBucket(bucketName)
                .WithObject(objectName)
                .WithStreamData(data)
                .WithObjectSize(data.Length)
                .WithContentType(contentType);

            await _minioClient.PutObjectAsync(args, cancellationToken).ConfigureAwait(false);
        }

        public async Task<Stream> GetObjectAsync(string bucketName, string objectName, CancellationToken cancellationToken = default)
        {
            var memoryStream = new MemoryStream();
            var args = new GetObjectArgs()
                .WithBucket(bucketName)
                .WithObject(objectName)
                .WithCallbackStream(stream => stream.CopyTo(memoryStream));

            await _minioClient.GetObjectAsync(args, cancellationToken).ConfigureAwait(false);
            memoryStream.Position = 0;
            return memoryStream;
        }

        public async Task RemoveObjectAsync(string bucketName, string objectName, CancellationToken cancellationToken = default)
        {
            var args = new RemoveObjectArgs()
                .WithBucket(bucketName)
                .WithObject(objectName);

            await _minioClient.RemoveObjectAsync(args, cancellationToken).ConfigureAwait(false);
        }
    }
}