using Npgsql;
using WebAPI.Data.Dto.ConfigDtos;
using WebAPI.Data.Enum;
using WebAPI.Extensions;

namespace WebAPI.Utilities.Helpers;

public static class AppConfig
{
    public static MailCreds MailCreds = null!;
    public static Mail MailBody = null!;
    public static string DevConnectionString = null!;
    public static AppInfo AppInfo = null!;

    public static string WebRootPath =>
        ServiceTool.ServiceProvider!.GetRequiredService<IWebHostEnvironment>().WebRootPath;


    public static void Build(IConfiguration conf)
    {
        MailCreds = conf.GetSection("MailCreds").Get<MailCreds>()!;
        AppInfo = conf.GetSection("AppInfo").Get<AppInfo>()!;
        MailBody = conf.GetSection("Mail").Get<Mail>()!;
        DevConnectionString = conf.GetConnectionString("DefaultConnection")
                              ?? throw new NpgsqlException(LangKeys.ConnectionStringError.Localize());
        //TODO tüm configler burada toplanacak
    }
}