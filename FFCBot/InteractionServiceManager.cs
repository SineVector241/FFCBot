using System.Reflection;
using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;

namespace FFCBot
{
    public class InteractionServiceManager
    {
        private readonly IServiceProvider _ServiceProvider;
        private readonly InteractionService BotInteractionService;
        private readonly DiscordSocketClient BotClient;

        public InteractionServiceManager(IServiceProvider Services)
        {
            _ServiceProvider = Services;
            BotInteractionService = _ServiceProvider.GetRequiredService<InteractionService>();
            BotClient = _ServiceProvider.GetRequiredService<DiscordSocketClient>();
        }

        public async Task Initialize()
        {
            try
            {
                await BotInteractionService.AddModulesAsync(Assembly.GetEntryAssembly(), _ServiceProvider);

                foreach (ModuleInfo module in BotInteractionService.Modules)
                {
                    Console.WriteLine($"\u001b[97m[{DateTime.Now}]: [\u001b[93mMODULES\u001b[97m] => {module.Name} \u001b[92mInitialized\u001b[97m");
                    BotInteractionService.Log += InteractionServiceLog;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\u001b[97m[{DateTime.Now}]: [\u001b[31mERROR\u001b[97m] => An error occured in InteractiveServiceManager.cs \nError Info:\n{ex}");
            }
        }

        private Task InteractionServiceLog(LogMessage msg)
        {
            Console.WriteLine(msg.Message);
            return Task.CompletedTask;
        }
    }
}