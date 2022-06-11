using Discord.WebSocket;
using izolabella.Discord.Objects.Arguments;
using izolabella.Discord.Objects.Constraints.Implementations;
using izolabella.Discord.Objects.Constraints.Interfaces;
using izolabella.Discord.Objects.Parameters;
using izolabella.One.Objects.Constants;
using izolabella.One.Objects.Entities;
using Kaia.Bot.Objects.Discord.Embeds.Bases;
using Kaia.Bot.Objects.Discord.Embeds.Implementations.CommandConstrained;
using Klara.Bot.Objects.Clients;

namespace izolabella.One.Objects.Controllers
{
    internal class KlaraBotController
    {
        internal KlaraBotController(KlaraBot Client)
        {
            this.Client = Client;
        }

        internal KlaraBot Client { get; }

        internal async Task StartController()
        {
            await this.Client.StartAsync();
        }
    }
}
