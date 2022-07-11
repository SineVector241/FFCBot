using Discord;
using Discord.Interactions;
using Discord.WebSocket;

namespace FFCBot.Core.SlashCommands
{
    [RequireUserPermission(GuildPermission.Administrator)]
    [Group("admin", "Admin Commands")]
    public class Admin : InteractionModuleBase<SocketInteractionContext<SocketInteraction>>
    {
        [SlashCommand("newverify", "Sends a new verification button")]
        public async Task NewVerifyButton(SocketRole AddRole, SocketRole? RemoveRole = null, string Message = "Verify Here")
        {
            await DeferAsync();
            if(RemoveRole == null)
            {
                var builder = new ComponentBuilder()
                    .WithButton("Verify", $"AVerify:{AddRole.Id}", ButtonStyle.Success);
                await Context.Channel.SendMessageAsync(Message, components: builder.Build());
                await FollowupAsync("Successfully Completed", ephemeral: true);
            }
            else
            {
                var builder = new ComponentBuilder()
                    .WithButton("Verify", $"ARVerify:{AddRole.Id},{RemoveRole.Id}", ButtonStyle.Success);
                await Context.Channel.SendMessageAsync(Message, components: builder.Build());
                await FollowupAsync("Successfully Completed", ephemeral: true);
            }
        }
    }
}
