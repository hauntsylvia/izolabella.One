using Discord.WebSocket;
using izolabella.CompetitiveCounting.Bot.Objects.CCB_Structures.Startup;
using izolabella.CompetitiveCounting.Bot.Objects.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace izolabella.CompetitiveCounting.Platform.Objects.Entry
{
    internal class EntryPoint
    {
        private EntryPoint(CompetitiveCountingBot Client)
        {
            this.Client = Client;
        }

        internal CompetitiveCountingBot Client { get; }

        private static async Task<List<CCBStartupInformation>> GetStartupProfilesAsync()
        {
            return await DataStores.StartupStore.ReadAllAsync<CCBStartupInformation>();
        }

        public static async Task<EntryPoint> BuildEntryPoint(DiscordSocketConfig Configuration)
        {
            List<CCBStartupInformation> StartupInformation = await GetStartupProfilesAsync();
            if(StartupInformation.Count == 0)
            {
                Console.WriteLine("Write the token.");
                string? Token = Console.ReadLine();
                Console.Clear();
                if(Token != null)
                {
                    CCBStartupInformation NewInfo = new(Token);
                    StartupInformation.Add(NewInfo);
                    await DataStores.StartupStore.SaveAsync(NewInfo);
                }
            }
            EntryPoint Entry = new(new(new(Configuration, StartupInformation.First().Token)));
            await Entry.Client.Parameters.StartAsync();
            return Entry;
        }
    }
}
