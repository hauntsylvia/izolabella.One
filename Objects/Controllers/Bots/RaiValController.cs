using Discord.WebSocket;
using izolabella.Discord.Objects.Arguments;
using izolabella.Discord.Objects.Constraints.Implementations;
using izolabella.Discord.Objects.Constraints.Interfaces;
using izolabella.Discord.Objects.Parameters;
using izolabella.Kaia.Bot.Objects.Discord.Embeds.Bases;
using izolabella.One.Objects.Constants;
using izolabella.Util.Controllers.Profiles;
using izolabella.Kaia.Bot.Objects.Discord.Embeds.Implementations.CommandConstrained;
using izolabella.RaiVal.Bot.Structures.Clients;

namespace izolabella.One.Objects.Controllers.Bots
{
    internal class RaiValController : Controller
    {
        public override string Name => "RaiVal";

        internal RaiValBot? B { get; private set; }

        protected override async Task StartProtectedAsync(ControllerProfile Profile)
        {
            this.B = new(Profile.Token, ConfigDefaults.DefaultConfig);
            this.B.Client.OnCommandConstraint += this.OnCommandConstraintAsync;
            await this.B.StartAsync();
        }

        protected override async Task StopProtectedAsync()
        {
            await (this.B?.StopAsync() ?? Task.CompletedTask);
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