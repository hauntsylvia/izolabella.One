using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace izolabella.One.Objects.Constants
{
    internal class ConfigDefaults
    {
        internal static DiscordSocketConfig DefaultConfig => new()
        {
            UseSystemClock = false,
            MessageCacheSize = 20,
            AlwaysDownloadUsers = true,
            AlwaysDownloadDefaultStickers = true,
            AlwaysResolveStickers = true,
            UseInteractionSnowflakeDate = false,
        };
    }
}
