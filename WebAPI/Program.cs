using Microsoft.AspNetCore;

namespace WebAPI;

public abstract class Program
{
    public static async Task Main(string[] args)
    {
        //asenkronsuz da calısabilir
        var app = CreateWebHostBuilder(args).Build();
        await app.RunAsync();
    }

    private static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>();
}