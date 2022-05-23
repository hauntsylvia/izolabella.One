using izolabella.CompetitiveCounting.Bot.Objects.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            this.Client.Parameters.CommandHandler.OnCommandConstraint += this.Wrapper_OnCommandConstraint;
            await this.Client.Parameters.StartAsync();
        }

        private Task Wrapper_OnCommandConstraint(Discord.Objects.Arguments.CommandContext Context, Discord.Objects.Parameters.IzolabellaCommandArgument[] Arguments, Discord.Objects.Constraints.Interfaces.IIzolabellaCommandConstraint ConstraintThatFailed)
        {
            throw new NotImplementedException();
        }
    }
}
