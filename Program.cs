global using Kaia.Bot.Objects.Clients;
global using Kaia.Bot.Objects.KaiaStructures.Startup;
using izolabella.One.Objects.Controllers;
using izolabella.One.Objects.Entry;

namespace izolabella.One
{
    internal class Program
    {
        internal static async Task Main()
        {
            KaiaBotController InternalController = await EntryPoint.EnterAsync(new()
            {
                UseSystemClock = false,
                MessageCacheSize = 20,
                AlwaysDownloadUsers = true,
                AlwaysDownloadDefaultStickers = true,
                AlwaysResolveStickers = true,
                UseInteractionSnowflakeDate = false,
            });
            await InternalController.StartController();
            await Task.Delay(-1);
        }
    }
}