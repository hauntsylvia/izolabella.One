using Discord.WebSocket;
using izolabella.One.Objects.Constants;
using izolabella.One.Objects.Controllers;

namespace izolabella.One.Objects.Entry
{
    internal class EntryPoint
    {
        public EntryPoint()
        {
            this.KaiaController = new KaiaBotController(new(new(Config: DefaultConfig, AllowBotsOnMessageReceivers: false, GlobalCommands: true, this.Profiles.ElementAt(0).Token)));
            this.KlaraController = new(new(this.Profiles.ElementAt(1).Token));
        }

        private static DiscordSocketConfig DefaultConfig => new()
        {
            UseSystemClock = false,
            MessageCacheSize = 20,
            AlwaysDownloadUsers = true,
            AlwaysDownloadDefaultStickers = true,
            AlwaysResolveStickers = true,
            UseInteractionSnowflakeDate = false,
        };

        public List<StartupProfile> Profiles { get; } = ValidifyStartupProfiles().Result;
        public KaiaBotController KaiaController { get; }
        public KlaraBotController KlaraController { get; }

        private static async Task<List<StartupProfile>> GetStartupProfilesAsync()
        {
            return await DataStores.StartupStore.ReadAllAsync<StartupProfile>();
        }

        public static async Task<List<StartupProfile>> ValidifyStartupProfiles(int NumberThereShouldBe = 2)
        {
            List<StartupProfile> StartupInformation = await GetStartupProfilesAsync();
            for(int I = 0; I < NumberThereShouldBe; I++)
            {
                if(StartupInformation.ElementAtOrDefault(I) == null)
                {
                    Console.WriteLine($"Write the token for profile {I}.");
                    string? Token = Console.ReadLine();
                    Console.Clear();
                    if (Token != null)
                    {
                        StartupProfile NewInfo = new(DataStores.StartupStore, Token);
                        StartupInformation.Add(NewInfo);
                        await DataStores.StartupStore.SaveAsync(NewInfo);
                        StartupInformation.Insert(I, NewInfo);
                    }
                }
            }
            return StartupInformation;
        }
    }
}
