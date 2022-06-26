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
