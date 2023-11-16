namespace WebAPI.Configuration;

public static class CoreModule
{
    public static void ConfigureCoreModule(this IServiceCollection services)
    {
        services.AddMemoryCache();
        services.AddHttpContextAccessor();
        services.AddHttpClient();
    }
}