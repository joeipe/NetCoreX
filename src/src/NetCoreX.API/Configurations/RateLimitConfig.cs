using System.Threading.RateLimiting;
using Microsoft.AspNetCore.RateLimiting;

namespace NetCoreX.API.Configurations
{
    public static class RateLimitConfig
    {
        public static void AddRateLimitConfiguration(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services);

            services.AddRateLimiter(options =>
            {
                options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
                    RateLimitPartition.GetFixedWindowLimiter(
                        partitionKey: httpContext.User?.Identity?.Name ?? httpContext.Connection.RemoteIpAddress?.ToString() ?? "anonymous",
                        factory: _ => new FixedWindowRateLimiterOptions
                        {
                            AutoReplenishment = true,
                            PermitLimit = 100,
                            Window = TimeSpan.FromMinutes(1)
                        }));

                options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
            });
        }

        public static void ApplyRateLimiting(this IApplicationBuilder app)
        {
            ArgumentNullException.ThrowIfNull(app);
            app.UseRateLimiter();
        }
    }
}
