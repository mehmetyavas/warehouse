using System.Security.Authentication;
using System.Security.Claims;
using System.Security.Principal;
using System.Text.Json.Serialization;
using Microsoft.Extensions.FileProviders;
using Swashbuckle.AspNetCore.SwaggerUI;
using WebAPI.Attributes;
using WebAPI.Configuration;
using WebAPI.Data;
using WebAPI.Data.Enum;
using WebAPI.Utilities.Helpers;
using WebAPI.Utilities.Middleware;
using WebAPI.Utilities.Security.Jwt;

namespace WebAPI;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            });
        services.AddEndpointsApiExplorer();

        services.ConfigureSwagger();

        services.ConfigureCors();

        services.ConfigureAuthentication(_configuration);

        services.AddAuthorization();

        AppConfig.Build(_configuration);


        ClaimsPrincipal GetPrincipal(IServiceProvider sp) =>
            sp.GetService<IHttpContextAccessor>()?.HttpContext?.User ??
            new ClaimsPrincipal(new ClaimsIdentity("Unknown"));

        services.AddScoped<IPrincipal>(GetPrincipal);

        services.AddTransient<ITokenHelper, JwtHelper>();


        services.ConfigureFilters();

        services.ConfigureCoreModule();


        services.AddAutoMapper(typeof(Startup));


        //CustomServices
        services.AddDbContext<AppDbContext>();

        services.AddScoped<UnitOfWork>();
        services.ConfigureServices();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        ServiceTool.ServiceProvider = app.ApplicationServices;

        //permissions 
        _ = app.ActionData();

        app.CreateFiles();


        app.UseDeveloperExceptionPage();

        app.UseMiddleware<ExceptionMiddleware>();


        app.UseSwagger();

        app.UseSwaggerUI(c =>
        {
            // DefaultArea Swagger belgesi
            typeof(ApiGroupNames).GetFields()
                .Skip(1)
                .ToList()
                .ForEach(f =>
                {
                    //Gets the attribute on the enumeration value
                    var info = f.GetCustomAttributes(typeof(GroupInfoAttribute), false)
                        .OfType<GroupInfoAttribute>()
                        .FirstOrDefault();
                    c.SwaggerEndpoint($"/swagger/{f.Name}/swagger.json", info != null ? info.Title : f.Name);
                });
            c.DocExpansion(DocExpansion.None);
        });


        app.UseCors("AllowOrigin");

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthentication();

        app.UseAuthorization();

        app.UseStaticFiles(new StaticFileOptions
        {
            OnPrepareResponse = (context) =>
            {
                if (!IsAuthenticated(context.Context))
                    throw new AuthenticationException(LangKeys.AuthorizationsDenied.ToString());
            },
            FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath,
                $"wwwroot/{FileDirectory.Image.ToString()}")),
            RequestPath = "/images"
        });
        
        app.UseStaticFiles(new StaticFileOptions
        {
            OnPrepareResponse = (context) =>
            {
                if (!IsAuthenticated(context.Context))
                    throw new AuthenticationException(LangKeys.AuthorizationsDenied.ToString());
            },
            FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath,
                $"wwwroot/{FileDirectory.File.ToString()}")),
            RequestPath = "/files"
        });
        // //app.useStaticFiles

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }

    private bool IsAuthenticated(HttpContext context)
    {
        if (!context.User.Identity!.IsAuthenticated)
            return false;
    
        // var url = context.Request.Path.Value;
        //
        //
        // var claim = context.User
        //     .GetTokenProps(JwtClaimConsts.AvatarUrl.ToString())
        //     .First();
        // if (url != null &&
        //     (/*context.User.Identity is null ||*/
        //      string.IsNullOrWhiteSpace(claim) ||
        //      !url.EndsWith(claim)))
        //     return false;
        return true;
    }
}