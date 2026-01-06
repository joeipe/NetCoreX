using Microsoft.AspNetCore.Http.HttpResults;

namespace NetCoreX.API.EndpointsHandler
{
    public static class AdminHandler
    {
        public static async Task<Ok<string>> GetEnvAsync(
           IConfiguration configuration)
        {
            var env = configuration["CurretEnv"];

            return TypedResults.Ok(env);
        }
    }
}