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
using Discord;
using izolabella.CompetitiveCounting.Bot.Objects.Discord.Embeds.Base;

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
            CCBEmbed Builder = new CommandConstrainedByUserIds(Context.UserContext.CommandName);
            if(Context.UserContext.User is SocketGuildUser SUser)
            {
                if(ConstraintThatFailed is WhitelistPermissionsConstraint WPC)
                {
                    Builder = new CommandConstrainedByPermissions(Context.UserContext.CommandName, SUser.GuildPermissions, WPC.Permissions);
                }
                else if(ConstraintThatFailed is WhitelistRolesConstraint RPC)
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
