using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection.Extensions;
using WebAPI.ActionFilters;
using WebAPI.Business.Auth.Service;
using WebAPI.Business.Mail;
using WebAPI.Business.User.Service;
using WebAPI.Services;
using WebAPI.Services.FileUploader;
using WebAPI.Services.User;

namespace WebAPI.Configuration;

public static class ServiceConfiguration
{
    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddScoped<FileService>();
        services.AddScoped<MailManager>();
        services.AddScoped<UserResolverServices>();
        services.AddScoped<PermissionService>();


        services.AddScoped<IAuthService, AuthManager>();
        services.AddScoped<IUserService, UserManager>();


        services.TryAddEnumerable(ServiceDescriptor.Singleton<IFilterProvider, BaseFilterProvider>());
    }
}