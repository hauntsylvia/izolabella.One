using Discord.WebSocket;
using izolabella.Discord.Objects.Arguments;
using izolabella.Discord.Objects.Constraints.Implementations;
using izolabella.Discord.Objects.Constraints.Interfaces;
using izolabella.Discord.Objects.Parameters;
using izolabella.One.Objects.Constants;
using izolabella.One.Objects.Controllers.Interfaces;
using izolabella.One.Objects.Profiles;
using Kaia.Bot.Objects.Discord.Embeds.Bases;
using Kaia.Bot.Objects.Discord.Embeds.Implementations.CommandConstrained;

namespace izolabella.One.Objects.Controllers.Implementations
{
    internal class KaiaController : IController
    {
        public string Alias => "Kaia";

        public bool Enabled { get; private set; } = false;

        internal KaiaBot? B { get; private set; }

        async Task IController.StartAsync(ControllerProfile Profile)
        {
            this.Enabled = true;
            this.B = new(new(ConfigDefaults.DefaultConfig, true, true, Profile.DiscordBotToken));
            this.B.Parameters.CommandHandler.OnCommandConstraint += OnCommandConstraintAsync;
            await this.B.Parameters.StartAsync();
        }

        async Task IController.StopAsync()
        {
            this.Enabled = false;
            await (this.B?.Parameters.StopAsync() ?? Task.CompletedTask);
        }

        private async Task OnCommandConstraintAsync(CommandContext Context, IzolabellaCommandArgument[] Arguments, IIzolabellaCommandConstraint ConstraintThatFailed)
        {
            KaiaPathEmbed Builder = new CommandConstrainedByUserIds(Kaia.Bot.Objects.Constants.Strings.EmbedStrings.FakePaths.Global, Context.UserContext.CommandName);
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
            await Context.UserContext.RespondAsync(
                embed: Builder.Build(),
                text: Strings.Responses.CommandConstrained);
        }
    }
}
