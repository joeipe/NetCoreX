using Microsoft.Extensions.Caching.Hybrid;

namespace NetCoreX.API.Configurations
{
    public static class CacheConfig
    {
        public static void AddCacheConfiguration(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));
            if (environment == null) throw new ArgumentNullException(nameof(environment));

            if (!environment.IsDevelopment())
            {
                services.AddStackExchangeRedisCache(options =>
                {
                    options.Configuration = configuration.GetConnectionString("RedisConnectionString");
                    options.InstanceName = "master";
                });
            }

            services.AddHybridCache(options =>
            {
                // Maximum size of cached items
                options.MaximumPayloadBytes = 1024 * 1024 * 2; // 2MB
                options.MaximumKeyLength = 256;

                // Default timeouts
                options.DefaultEntryOptions = new HybridCacheEntryOptions
                {
                    Expiration = TimeSpan.FromSeconds(30),
                    LocalCacheExpiration = TimeSpan.FromSeconds(30),
                };
            });
        }
    }
}
