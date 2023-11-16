using WebAPI.Data.Enum;

namespace WebAPI.Configuration;

public static class FileConfiguration
{
    public static void CreateFiles(this IApplicationBuilder app)
    {
        var getFileEnum = Enum.GetNames(typeof(FileDirectory)).ToList();

        var baseDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

        if (!Directory.Exists(baseDir))
            Directory.CreateDirectory(baseDir);

        foreach (var file in getFileEnum)
        {
            var fileDir = Path.Combine(baseDir, file);
            if (!Directory.Exists(fileDir))
                Directory.CreateDirectory(fileDir);
        }
    }
}