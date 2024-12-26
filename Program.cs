using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

public class Program {
    public static async Task Main(string[] args) {
        IHostBuilder builder = Host.CreateDefaultBuilder(args);
        DiscordService discord;

        builder.ConfigureLogging(logging => {
            #if DEBUG
            logging.ClearProviders().AddDebug();
            #else
            logging.ClearProviders().AddConsole();
            #endif
        });

        builder.ConfigureServices(services => {
            services.AddSingleton<DiscordService>();

            services.AddHostedService<SqliteDataStore>();
        });

        builder.ConfigureAppConfiguration(configuration => {
            configuration.SetBasePath(Directory.GetCurrentDirectory());
            configuration.AddJsonFile("appsettings.json");
            
        });

        using IHost host = builder.Build();
        
        discord = host.Services.GetService<DiscordService>()!;

        discord.Configure();

        await discord.Login();

        await host.RunAsync();
    }
}