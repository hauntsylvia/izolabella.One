﻿using Discord.WebSocket;
using izolabella.Discord.Objects.Arguments;
using izolabella.Discord.Objects.Constraints.Implementations;
using izolabella.Discord.Objects.Constraints.Interfaces;
using izolabella.Discord.Objects.Parameters;
using izolabella.One.Objects.Constants;
using izolabella.One.Objects.Entities;
using Kaia.Bot.Objects.Discord.Embeds.Bases;
using Kaia.Bot.Objects.Discord.Embeds.Implementations.CommandConstrained;

namespace izolabella.One.Objects.Controllers
{
    internal class KaiaBotController
    {
        internal KaiaBotController(KaiaBot Client)
        {
            this.Client = Client;
        }

        internal KaiaBot Client { get; }

        internal async Task StartController()
        {
            this.Client.Parameters.CommandHandler.OnCommandConstraint += this.OnCommandConstraintAsync;
            await this.Client.Parameters.StartAsync();
        }

        private async Task OnCommandConstraintAsync(CommandContext Context, IzolabellaCommandArgument[] Arguments, IIzolabellaCommandConstraint ConstraintThatFailed)
        {
            await DataStores.ConstrainmentStore.SaveAsync(new CommandLog(Context.UserContext.User.Id));
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