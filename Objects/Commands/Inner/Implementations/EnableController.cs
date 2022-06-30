using izolabella.One.Objects.Commands.Inner.Interfaces;
using izolabella.One.Objects.Constants;
using izolabella.One.Objects.Controllers.Interfaces;
using izolabella.One.Objects.Profiles;

namespace izolabella.One.Objects.Commands.Inner.Implementations
{
    internal class EnableController : IIzolabellaConsoleCommand
    {
        string IIzolabellaConsoleCommand.RequiredName => "enable";

        async Task<string> IIzolabellaConsoleCommand.RunAsync(string[] Args)
        {
            string Alias = Args.ElementAtOrDefault(1) ?? string.Empty;
            if (Alias.ToLower() == "all")
            {
                foreach (IController Controller in Program.KnownControllers)
                {
                    if (Controller.Enabled)
                    {
                        await Controller.StopAsync();
                    }
                }
                Console.Clear();
                await Program.Main();
                return "New main created.";
            }
            else
            {
                IController? C = Program.KnownControllers.FirstOrDefault(KC => KC.Alias.ToLower() == (Args.ElementAtOrDefault(1) ?? string.Empty).ToLower());
                if (C != null)
                {
                    ControllerProfile? CProfile = (await DataStores.ControllerProfileStore.ReadAllAsync<ControllerProfile>()).FirstOrDefault(CPrf => CPrf.Alias == C.Alias);
                    if (CProfile != null)
                    {
                        if(!C.Enabled)
                        {
                            await C.StartAsync(CProfile);
                            return "Controller enabled.";
                        }
                        else
                        {
                            return "Controller already enabled.";
                        }
                    }
                    else
                    {
                        return "No controller profile matching the provided alias was found for this controller.";
                    }
                }
                else
                {
                    return "No controller matching the alias provided was found.";
                }
            }
        }
    }
}
