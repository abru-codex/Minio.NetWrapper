# Minio.NetWrapper

Minio.NetWrapper is a .NET library that simplifies interaction with MinIO object storage.

## 🚀 Features
- Create and manage buckets
- Upload and download objects
- Asynchronous support
- Configurable via dependency injection

## 📦 Installation

1. **Clone the Repository:**

```bash
git clone https://github.com/abru-codex/Minio.NetWrapper.git
```

2. **Add Project Reference:**

In your .NET project, add a reference to the MinIO library:


3. **Configure Using Dependency Injection:**

In your `Program.cs` or `Startup.cs`, add the MinIO storage service to the service container:

```csharp
using Minio.NetWrapper;
using Minio.NetWrapper.Models;

var builder = WebApplication.CreateBuilder(args);

// Add MinIO Storage Service
builder.Services.AddMinioStorage(options =>
{
    options.Endpoint = "https://play.min.io";
    options.AccessKey = "YOUR_ACCESS_KEY";
    options.SecretKey = "YOUR_SECRET_KEY";
    options.WithSSL = true;
});

var app = builder.Build();
app.Run();
```

4. **This library provides the following interface for interacting with MinIO:**

```csharp
public interface IMinioStorageService
{
    Task<bool> BucketExistsAsync(string bucketName, CancellationToken cancellationToken = default);
    Task CreateBucketAsync(string bucketName, CancellationToken cancellationToken = default);
    Task<string> GetPresignedUrlAsync(string bucketName, string objectName, int expiryInSeconds = 3600, CancellationToken cancellationToken = default);
    Task UploadObjectAsync(string bucketName, string objectName, Stream data, string contentType, CancellationToken cancellationToken = default);
    Task<Stream> GetObjectAsync(string bucketName, string objectName, CancellationToken cancellationToken = default);
    Task RemoveObjectAsync(string bucketName, string objectName, CancellationToken cancellationToken = default);
}
```

## 📄 License
This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for more information.
