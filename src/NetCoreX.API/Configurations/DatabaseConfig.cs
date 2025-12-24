using Microsoft.EntityFrameworkCore;
using NetCoreX.Data;

namespace NetCoreX.API.Configurations
{
    public static class DatabaseConfig
    {
        public static void AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddDbContext<NetCoreXDbContext>(options =>
                options
                    .UseSqlite(configuration.GetConnectionString("DBConnectionString"))
                    //.LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information)
                    //.LogTo(message => Log.Information(message), new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information)
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
