using System.Configuration;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

public class DiscordService(ILogger<DiscordService> logger, IConfiguration configuration)
{
    private DiscordSocketClient? m_client;

    public DiscordSocketClient GetClient() => m_client!;

    public void Configure() {
        m_client = new DiscordSocketClient();
        m_client.Log += message => {
            switch (message.Severity) {
                case Discord.LogSeverity.Info:
                    logger.LogInformation(message.ToString());
                    break;
                case Discord.LogSeverity.Error:
                    logger.LogError(message.ToString());
                    break;
                case Discord.LogSeverity.Warning:
                    logger.LogWarning(message.ToString());
                    break;
                case Discord.LogSeverity.Critical:
                    logger.LogCritical(message.ToString());
                    break;
                case Discord.LogSeverity.Verbose:
                    logger.LogDebug(message.ToString());
                    break;
            }

            return Task.CompletedTask;
        };
    }

    public async Task Login()
    {
        await m_client!.LoginAsync(Discord.TokenType.Bot, configuration.GetValue<string>("DiscordConfiguration:client-token"));
        await m_client.StartAsync();
    }

    public void RegisterEvents() {
        m_client!.UserJoined += async (user) => {
            await user.Guild.GetTextChannel(0).SendMessageAsync();
        };
    }
}