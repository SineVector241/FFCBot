using Discord;
using Discord.WebSocket;
using Discord.Interactions;

namespace FFCBot.Core.SlashCommands
{
    public class Verification : InteractionModuleBase<SocketInteractionContext<SocketInteraction>>
    {
        [SlashCommand("gamertag", "Verify your gamertag")]
        [RequireContext(ContextType.Guild)]
        public async Task GamertagVerify(string Gamertag, 
            [Choice("Pocket Edition", "Pocket Edition"), 
            Choice("Xbox Edition", "Xbox Edition"), 
            Choice("IOS Edition", "IOS Edition"), 
            Choice("Windows 10 Edition", "Windows 10 Edition"),
            Choice("Playstation Edition", "Playstation Edition")] string Platform)
        {
            var user = Context.User as SocketGuildUser;
            if(user.Roles.FirstOrDefault(x => x.Id == 856100292176248872) == null)
            {
                await Context.Guild.GetTextChannel(786907258143244288).SendMessageAsync(Context.User.Mention, embed: new EmbedBuilder()
                    .WithTitle($"Gamertag: {Context.User.Username}")
                    .AddField("Platform", Platform)
                    .AddField("Gamertag", Gamertag)
                    .WithAuthor(Context.User)
                    .WithTimestamp(DateTime.UtcNow)
                    .WithColor(Color.Gold)
                    .Build());
                await user.AddRoleAsync(856100292176248872);
                await RespondAsync("You have been verified!", ephemeral: true);
            }
            else
            {
                await RespondAsync("You have already been verified!", ephemeral: true);
            }
        }
    }
}
