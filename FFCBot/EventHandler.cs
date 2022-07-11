using Discord.WebSocket;
using Discord.Interactions;
using Microsoft.Extensions.DependencyInjection;
using Discord;

namespace FFCBot
{
    public class EventHandler
    {
        private DiscordSocketClient BotClient;
        private readonly InteractionService BotInteractionService;
        private readonly IServiceProvider _ServiceProvider;

        public EventHandler(IServiceProvider Services)
        {
            _ServiceProvider = Services;
            BotInteractionService = Services.GetRequiredService<InteractionService>();
            BotClient = Services.GetRequiredService<DiscordSocketClient>();
        }

        public Task Initialize()
        {
            BotClient.Ready += Ready;
            BotClient.InteractionCreated += InteractionCreated;
            return Task.CompletedTask;
        }

        private async Task InteractionCreated(SocketInteraction interaction)
        {
            try
            {
                if (interaction is SocketMessageComponent)
                {
                    var ctx = new SocketInteractionContext<SocketMessageComponent>(BotClient, (SocketMessageComponent)interaction);
                    var res = await BotInteractionService.ExecuteCommandAsync(ctx, _ServiceProvider);
                    if(!res.IsSuccess)
                    {
                        Console.WriteLine(res.ErrorReason);
                    }
                }
                else
                {
                    var ctx = new SocketInteractionContext<SocketInteraction>(BotClient, interaction);
                    var res = await BotInteractionService.ExecuteCommandAsync(ctx, _ServiceProvider);
                    if (!res.IsSuccess)
                    {
                        Console.WriteLine(res.ErrorReason);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private async Task Ready()
        {
            try
            {
                Console.WriteLine($"\u001b[97m[{DateTime.Now}]: [\u001b[92mREADY\u001b[97m] => {BotClient.CurrentUser.Username} is ready!");
                await BotClient.SetGameAsync("/help");
                await BotClient.SetStatusAsync(UserStatus.Online);
#if DEBUG
                await BotInteractionService.RegisterCommandsToGuildAsync(786907257732333568);
#else
                await BotInteractionService.RegisterCommandsGloballyAsync();
#endif
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\u001b[97m[{DateTime.Now}]: [\u001b[31mERROR\u001b[97m] => An error occured in EventHandler.cs \nError Info:\n{ex}");
            }
        }
    }
}