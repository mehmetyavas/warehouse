using System.Reflection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using WebAPI.Attributes;
using WebAPI.Data.Enum;

namespace WebAPI.Configuration;

public static class SwaggerConfiguration
{
    public static void ConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            typeof(ApiGroupNames).GetFields()
                .ToList()
                .ForEach(f =>
                {
                    var info = f.GetCustomAttributes(typeof(GroupInfoAttribute), false)
                        .OfType<GroupInfoAttribute>()
                        .FirstOrDefault();
                    c.SwaggerDoc(f.Name, new OpenApiInfo()
                    {
                        Title = info?.Title,
                        Version = info?.Version,
                        Description = info?.Description,
                    });
                });


            //TODO: Refactor
            c.DocInclusionPredicate((docName, apiDesc) =>
            {
                if (!apiDesc.TryGetMethodInfo(out MethodInfo methodInfo)) return false;


                var backOffice = apiDesc.ActionDescriptor.EndpointMetadata.Any(x => x is BackOfficeAttribute);

                if (docName == ApiGroupNames.All.ToString())
                {
                    return true;
                }

                if (docName == ApiGroupNames.BackOffice.ToString() &&
                    backOffice)
                {
                    return true;
                }

                if (docName == ApiGroupNames.Default.ToString() &&
                    !backOffice)
                {
                    return true;
                }

                return false;
            });


            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description =
                    "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                    },
                    new List<string>()
                }
            });
            c.IncludeXmlComments(Path.ChangeExtension(typeof(Startup).Assembly.Location, ".xml"));
            //  var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            //  var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);
            // c.IncludeXmlComments(xmlPath);
        });
    }
}