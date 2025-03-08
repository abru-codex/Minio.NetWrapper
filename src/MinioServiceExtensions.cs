using Microsoft.Extensions.DependencyInjection;
using Minio;

namespace MinioStorage
{
    public static class MinioServiceExtensions
    {
        public static IServiceCollection AddMinioStorage(this IServiceCollection services, Action<MinioOptions> configureOptions)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (configureOptions == null) throw new ArgumentNullException(nameof(configureOptions));

            services.Configure(configureOptions);
            services.AddSingleton<IMinioClient>(provider =>
            {
                var options = provider.GetRequiredService<Microsoft.Extensions.Options.IOptions<MinioOptions>>().Value;

                return new MinioClient()
                    .WithEndpoint(options.Endpoint)
                    .WithCredentials(options.AccessKey, options.SecretKey)
                    .WithSessionToken(options.SessionToken)
                    .WithSSL(options.WithSSL)
                    .Build();
            });

            services.AddSingleton<IMinioStorageService, MinioStorageService>();

            return services;
        }
    }
}