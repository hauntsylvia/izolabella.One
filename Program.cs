global using Kaia.Bot.Objects.KaiaStructures.Startup;
global using Kaia.Bot.Objects.Clients;
using izolabella.One.Objects.Controllers;
using izolabella.One.Objects.Entry;
using System.Reflection;
using izolabella.Discord.Objects.Interfaces;

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