using WebAPI.ActionFilters;

namespace WebAPI.Configuration;

public static class FilterConfiguration
{
    public static void ConfigureFilters(this IServiceCollection services)
    {
        services.AddMvc(
            options =>
            {
                options.Filters.Add(typeof(ModelStateFilter), order: 1);
                options.EnableEndpointRouting = true;
            });
    }
}