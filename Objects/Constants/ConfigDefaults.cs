using Discord;
using Discord.WebSocket;

namespace izolabella.One.Objects.Constants
{
    internal sealed class  ConfigDefaults
    {
        internal static DiscordSocketConfig DefaultConfig => new()
        {
            UseSystemClock = false,
            MessageCacheSize = 20,
            AlwaysDownloadUsers = true,
            AlwaysDownloadDefaultStickers = true,
            AlwaysResolveStickers = true,
            UseInteractionSnowflakeDate = false,
            GatewayIntents = GatewayIntents.All
        };
    }
}