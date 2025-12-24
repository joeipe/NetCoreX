using Microsoft.AspNetCore.Authorization;

namespace NetCoreX.API.Configurations
{
    public static class AuthConfig
    {
        public static void AddAuthenticationConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            services.AddAuthentication().AddJwtBearer();
        }

        public static void AddAuthorizationConfiguration(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddAuthorization();

            services.AddAuthorizationBuilder()
                .AddPolicy("MustBeAdminFromAu", policy =>
                {
                    policy.RequireRole("admin");
                    policy.RequireClaim("country", "australia");
                });
        }

        public static void ApplyAuth(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
        }
    }
}
