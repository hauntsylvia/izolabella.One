using Discord.WebSocket;
using izolabella.Discord.Objects.Arguments;
using izolabella.Discord.Objects.Constraints.Implementations;
using izolabella.Discord.Objects.Constraints.Interfaces;
using izolabella.Discord.Objects.Parameters;
using izolabella.One.Objects.Constants;
using izolabella.Util.Controllers.Profiles;
using Kaia.Bot.Objects.Discord.Embeds.Bases;
using Kaia.Bot.Objects.Discord.Embeds.Implementations.CommandConstrained;

namespace izolabella.One.Objects.Controllers
{
    internal class KaiaController : Controller
    {
        public override string Alias => "Kaia";

        internal KaiaBot? B { get; private set; }

        protected async override Task StartProtectedAsync(ControllerProfile Profile)
        {
            this.B = new(this, new(ConfigDefaults.DefaultConfig, true, true, Profile.Token));
            this.B.Parameters.CommandHandler.OnCommandConstraint += this.OnCommandConstraintAsync;
            await this.B.Parameters.StartAsync();
        }

        protected async override Task StopProtectedAsync()
        {
            await (this.B?.Parameters.StopAsync() ?? Task.CompletedTask);
        }

        private async Task OnCommandConstraintAsync(CommandContext Context, IzolabellaCommandArgument[] Arguments, IIzolabellaCommandConstraint ConstraintThatFailed)
        {
            KaiaPathEmbedRefreshable Builder = new CommandConstrainedByUserIds(Kaia.Bot.Objects.Constants.Strings.EmbedStrings.FakePaths.Global, Context.UserContext.CommandName);
            if (Context.UserContext.User is SocketGuildUser SUser)
            {
                if (ConstraintThatFailed is WhitelistPermissionsConstraint WPC)
                {
                    Builder = new CommandConstrainedByPermissions(SUser.Guild.Name, Context.UserContext.CommandName, SUser.GuildPermissions, WPC.Permissions);
                }
                else if (ConstraintThatFailed is WhitelistRolesConstraint RPC)
                {
                    Builder = new CommandConstrainedByRoleIds(Context.UserContext.CommandName, SUser.Guild, RPC.RoleIds);
                }
            }
            await Builder.RefreshAsync();
            await Context.UserContext.RespondAsync(
                embed: Builder.Build(),
                text: Strings.Responses.CommandConstrained);
        }
    }
}
