using izolabella.CompetitiveCounting.Bot.Objects.Clients;
using izolabella.CompetitiveCounting.Platform.Objects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using izolabella.Discord.Objects.Enums;
using izolabella.Discord.Objects.Arguments;
using izolabella.Discord.Objects.Constraints.Interfaces;
using izolabella.Discord.Objects.Parameters;
using izolabella.CompetitiveCounting.Bot.Objects.Discord.Embeds.Implementations;
using Discord.WebSocket;
using izolabella.Discord.Objects.Constraints.Implementations;

namespace izolabella.CompetitiveCounting.Platform.Objects.Controllers
{
    internal class CCBotController
    {
        internal CCBotController(CompetitiveCountingBot Client)
        {
            this.Client = Client;
        }

        internal CompetitiveCountingBot Client { get; }

        internal async Task StartController()
        {
            this.Client.Parameters.CommandHandler.OnCommandConstraint += this.OnCommandConstraintAsync;
            await this.Client.Parameters.StartAsync();
        }

        private async Task OnCommandConstraintAsync(CommandContext Context, IzolabellaCommandArgument[] Arguments, IIzolabellaCommandConstraint ConstraintThatFailed)
        {
            await DataStores.ConstrainmentStore.SaveAsync(new CommandLog(Context.UserContext.User.Id));
            await Context.UserContext.RespondAsync(
                embed:
                    ConstraintThatFailed.Type == ConstraintTypes.WhitelistPermissions && Context.UserContext.User is SocketGuildUser SUser && ConstraintThatFailed is WhitelistPermissionsConstraint WPC ?
                        new CommandConstrainedByPermissions(Context.UserContext.CommandName, SUser.GuildPermissions, WPC.Permissions).Build()
                        : null,
                text: Strings.Responses.CommandConstrained);
        }
    }
}
