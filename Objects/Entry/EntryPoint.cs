using Discord.WebSocket;
using izolabella.CompetitiveCounting.Bot.Objects.CCB_Structures.Startup;
using izolabella.CompetitiveCounting.Bot.Objects.Clients;
using izolabella.CompetitiveCounting.Platform.Objects.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace izolabella.CompetitiveCounting.Platform.Objects.Entry
{
    internal static class EntryPoint
    {
        private static async Task<List<CCBStartupInformation>> GetStartupProfilesAsync()
        {
            return await DataStores.StartupStore.ReadAllAsync<CCBStartupInformation>();
        }

        public static async Task<CCBotController> EnterAsync(DiscordSocketConfig Configuration)
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
            return new CCBotController(new(new(Configuration, StartupInformation.First().Token)));
        }
    }
}
