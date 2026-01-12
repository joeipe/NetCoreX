using Microsoft.EntityFrameworkCore;
using NetCoreX.Data;
using Serilog;

namespace NetCoreX.API.Configurations
{
    public static class DatabaseConfig
    {
        public static void AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            var basePath = Directory.GetCurrentDirectory();
            var dataDir = Path.Combine(basePath, "AppData");
            Directory.CreateDirectory(dataDir);

            var dbPath = Path.Combine(dataDir, "NetCoreXDb.db");

            services.AddDbContext<NetCoreXDbContext>(options =>
                options
                    //.UseSqlite(configuration.GetConnectionString("DBConnectionString"))
                    //.LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information)
                    .UseSqlite($"Data Source={dbPath}")
                    .LogTo(message => Log.Information(message), new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information)
                    .ConditionalEnableSensitiveDataLogging()
            //.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
            );
        }

        public static void ApplyDatabaseSchema(this IApplicationBuilder app, IWebHostEnvironment environment)
        {
            using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>()?.CreateScope();
            var retryCount = 0;
            const int maxRetries = 3;
            
            while (retryCount < maxRetries)
            {
                try
                {
                    var dbContext = serviceScope?.ServiceProvider.GetRequiredService<NetCoreXDbContext>();
                    dbContext?.Database.Migrate();
                    break;
                }
                catch (Exception ex)
                {
                    retryCount++;
                    if (retryCount >= maxRetries)
                    {
                        Log.Fatal(ex, "Failed to apply database migrations after {MaxRetries} attempts", maxRetries);
                        throw;
                    }
                    Log.Warning(ex, "Database migration failed. Retry {RetryCount} of {MaxRetries} in 15 seconds", retryCount, maxRetries);
                    Thread.Sleep(TimeSpan.FromSeconds(15));
                }
            }
        }

        private static DbContextOptionsBuilder ConditionalEnableSensitiveDataLogging(
            this DbContextOptionsBuilder optionsBuilder)
        {
            var isDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";
            if (isDevelopment)
            {
                optionsBuilder.EnableSensitiveDataLogging();
            }
            return optionsBuilder;
        }
    }
}