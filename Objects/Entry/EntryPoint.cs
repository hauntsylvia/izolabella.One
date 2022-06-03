using Discord.WebSocket;
using izolabella.One.Objects.Constants;
using izolabella.One.Objects.Controllers;

namespace izolabella.One.Objects.Entry
{
    internal static class EntryPoint
    {
        private static async Task<List<KaiaStartupInformation>> GetStartupProfilesAsync()
        {
            return await DataStores.StartupStore.ReadAllAsync<KaiaStartupInformation>();
        }

        public static async Task<KaiaBotController> EnterAsync(DiscordSocketConfig Configuration)
        {
            List<KaiaStartupInformation> StartupInformation = await GetStartupProfilesAsync();
            if (StartupInformation.Count == 0)
            {
                Console.WriteLine("Write the token.");
                string? Token = Console.ReadLine();
                Console.Clear();
                if (Token != null)
                {
                    KaiaStartupInformation NewInfo = new(DataStores.StartupStore, Token);
                    StartupInformation.Add(NewInfo);
                    await DataStores.StartupStore.SaveAsync(NewInfo);
                }
            }
            return new KaiaBotController(new(new(Config: Configuration, AllowBotsOnMessageReceivers: false, GlobalCommands: true, StartupInformation.First().Token)));
        }
    }
}
