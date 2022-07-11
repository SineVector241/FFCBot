using Discord;
using Discord.WebSocket;
using Discord.Interactions;

namespace FFCBot.Core.Interactions
{
    public class Common : InteractionModuleBase<SocketInteractionContext<SocketMessageComponent>>
    {
        [ComponentInteraction("AVerify:*")]
        public async Task AddRoleVerify(string AddRoleId)
        {
            var User = Context.Interaction.User as SocketGuildUser;
            if (User != null && User.Roles.FirstOrDefault(x => x.Id.ToString() == AddRoleId) == null)
            {
                await User.AddRoleAsync(Convert.ToUInt64(AddRoleId));
                await RespondAsync("Successfully Verified", ephemeral: true);
            }
            else
            {
                await RespondAsync("Cannot Verify. You have already been verified for this role", ephemeral: true);
            }
        }

        [ComponentInteraction("RAVerify:*,*")]
        public async Task RemoveAddRoleVerify(string AddRoleId, string RemoveRoleId)
        {
            var User = Context.Interaction.User as SocketGuildUser;
            if (User != null && User.Roles.FirstOrDefault(x => x.Id.ToString() == AddRoleId) == null && User.Roles.FirstOrDefault(x => x.Id.ToString() == RemoveRoleId) != null)
            {
                await User.AddRoleAsync(Convert.ToUInt64(AddRoleId));
                await User.RemoveRoleAsync(Convert.ToUInt64(RemoveRoleId));
                await RespondAsync("Successfully Verified", ephemeral: true);
            }
            else
            {
                await RespondAsync("Cannot Verify. You have already been verified for this role or you do not have the role required to verify", ephemeral: true);
            }
        }
    }
}
