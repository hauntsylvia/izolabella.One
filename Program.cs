global using izolabella.CompetitiveCounting.Platform.Objects.Constants;

using izolabella.CompetitiveCounting.Bot.Objects.Clients;
using izolabella.CompetitiveCounting.Platform.Objects.Controllers;
using izolabella.CompetitiveCounting.Platform.Objects.Entry;

namespace izolabella.CompetitiveCounting.Platform
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