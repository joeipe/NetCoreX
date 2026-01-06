using Microsoft.OpenApi;

namespace NetCoreX.API.Configurations
{
    public static class SwaggerConfig
    {
        private const string schemeId = "bearer";

        public static void AddSwaggerConfiguration(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            //services.AddOpenApi();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "NetCoreX API",
                    Version = "v1"
                });

                options.AddSecurityDefinition(schemeId, new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Description = "Token-based authentication and authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    In = ParameterLocation.Header
                });
                options.AddSecurityRequirement(document => new() { [new OpenApiSecuritySchemeReference(schemeId, document)] = [] });
            });
        }

        public static void ApplySwagger(this IApplicationBuilder app)
        {
            //app.MapOpenApi();
            //app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.jason", "v1"));

            app.UseSwagger();
            app.UseSwaggerUI();
        }
    }
}