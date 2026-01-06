namespace NetCoreX.API.Configurations
{
    public static class AppConfigurationConfig
    {
        public static void AddAppConfigurationConfiguration(this ConfigurationManager configuration, IWebHostEnvironment environment)
        {
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));
            if (environment == null) throw new ArgumentNullException(nameof(environment));

            if (!environment.IsDevelopment())
            {
                configuration.AddAzureAppConfiguration(options =>
                    options.Connect(configuration.GetConnectionString("AppConfigConnectionString")));
            }
        }

        public static void AddAppConfigurationConfiguration(this IServiceCollection services, IWebHostEnvironment environment)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (environment == null) throw new ArgumentNullException(nameof(environment));

            if (!environment.IsDevelopment())
            {
                services.AddAzureAppConfiguration();
            }
        }

        public static void ApplyAppConfiguration(this IApplicationBuilder app, IWebHostEnvironment environment)
        {
            if (environment == null) throw new ArgumentNullException(nameof(environment));

            if (!environment.IsDevelopment())
            {
                app.UseAzureAppConfiguration();
            }
        }
    }
}