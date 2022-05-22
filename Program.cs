global using izolabella.CompetitiveCounting.Platform.Objects.Constants;

using izolabella.CompetitiveCounting.Bot.Objects.Clients;
using izolabella.CompetitiveCounting.Platform.Objects.Entry;

namespace izolabella.CompetitiveCounting.Platform
{
    internal class Program
    {
        internal static async Task Main(string[] args)
        {
            EntryPoint Entry = await EntryPoint.BuildEntryPoint(new()
            {
                UseSystemClock = false,
                MessageCacheSize = 20,
                AlwaysDownloadUsers = true,
                AlwaysDownloadDefaultStickers = true,
                AlwaysResolveStickers = true,
                UseInteractionSnowflakeDate = false,
            });
            await Task.Delay(-1);
        }
    }
}