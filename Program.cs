global using Kaia.Bot.Objects.Clients;
global using Kaia.Bot.Objects.KaiaStructures.Startup;
using izolabella.One.Objects.Entry;

namespace izolabella.One
{
    internal class Program
    {
        internal static async Task Main()
        {
            EntryPoint E = new();
            await E.KaiaController.StartController();
            await E.KlaraController.StartController();
            await Task.Delay(-1);
        }
    }
}