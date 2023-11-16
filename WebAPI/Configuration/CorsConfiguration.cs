namespace WebAPI.Configuration;

public static class CorsConfiguration
{
    public static void ConfigureCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(
                "AllowOrigin",
                builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
        });

    }
}