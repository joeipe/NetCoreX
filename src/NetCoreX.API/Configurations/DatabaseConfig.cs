using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NetCoreX.Data;
using Serilog;

namespace NetCoreX.API.Configurations
{
    public static class DatabaseConfig
    {
        public static void AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            string folderName = "AppData";
            var fileName = "NetCoreXDb.db";
            var relativeFilePath = $@"{folderName}{fileName}";
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativeFilePath);

            services.AddDbContext<NetCoreXDbContext>(options =>
                options
                    //.UseSqlite(configuration.GetConnectionString("DBConnectionString"))
                    //.LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information)
                    .UseSqlite($"Data Source={filePath}")
                    .LogTo(message => Log.Information(message), new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information)
                    .EnableSensitiveDataLogging()
                    //.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
            );
        }

        public static void ApplyDatabaseSchema(this IApplicationBuilder app, IWebHostEnvironment environment)
        {
            using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>()?.CreateScope();
            try
            {
                var dbContext = serviceScope?.ServiceProvider.GetRequiredService<NetCoreXDbContext>();
                dbContext?.Database.Migrate();
            }
            catch (Exception)
            {
                Thread.Sleep(TimeSpan.FromSeconds(15));
                app.ApplyDatabaseSchema(environment);
            }
        }
    }
}
