using Discord;
using Discord.WebSocket;
using Discord.Interactions;
using Microsoft.Extensions.DependencyInjection;

namespace FFCBot
{
    public class Bot
    {
        private DiscordSocketClient BotClient;
        private IServiceProvider _ServiceProvider;
        private InteractionService BotInteractionService;

        public Bot()
        {
            BotClient = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Debug,
                UseInteractionSnowflakeDate = false,
                MessageCacheSize = 100
            });

            BotInteractionService = new InteractionService(BotClient, new InteractionServiceConfig
            {
                LogLevel = LogSeverity.Debug,
                DefaultRunMode = RunMode.Async,
                ThrowOnError = true
            });

            _ServiceProvider = BuildServiceProvider();
        }
        public async Task MainAsync()
        {
            await new EventHandler(_ServiceProvider).Initialize();
            await new InteractionServiceManager(_ServiceProvider).Initialize();
            BotClient.Log += ClientLog;

            if (string.IsNullOrWhiteSpace(Config.BotConfiguration.Token))
            {
                Console.WriteLine("\u001b[41mBOT CONFIGURATION TOKEN IS BLANK\u001b[40m");
                return;
            }

            await BotClient.LoginAsync(TokenType.Bot, Config.BotConfiguration.Token);
            await BotClient.StartAsync();

            await Task.Delay(-1);
        }

        private Task ClientLog(LogMessage msg)
        {
            Console.WriteLine($"\u001b[97m[{DateTime.Now}]: [\u001b[93m{msg.Source}\u001b[97m] => {msg.Message}");
            return Task.CompletedTask;
        }

        private ServiceProvider BuildServiceProvider()
        {
            return new ServiceCollection()
                .AddSingleton(BotClient)
                .AddSingleton(BotInteractionService)
                .BuildServiceProvider();
        }
    }
}