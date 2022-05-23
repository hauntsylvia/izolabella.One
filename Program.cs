global using Kaia.Bot.Objects.Clients;
global using Kaia.Bot.Objects.CCB_Structures.Guilds;
global using Kaia.Bot.Objects.CCB_Structures.Users;
global using Kaia.Bot.Objects.CCB_Structures.Derivations;
global using Kaia.Bot.Objects.CCB_Structures.Startup;

using izolabella.One.Objects.Controllers;
using izolabella.One.Objects.Entry;

namespace izolabella.One
{
    internal class Program
    {
        internal static async Task Main()
        {
            CCBotController InternalController = await EntryPoint.EnterAsync(new()
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