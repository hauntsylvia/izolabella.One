global using izolabella.Util.Controllers;
global using izolabella.Util.Controllers.Profiles;

using izolabella.One.Objects.Commands.Inner.Interfaces;
using izolabella.One.Objects.Constants;
using izolabella.Util.IzolabellaConsole;
using System.Globalization;

namespace izolabella.One.Objects.Commands.Inner.Implementations
{
    internal class EnableController : IIzolabellaConsoleCommand
    {
        internal override string RequiredName => "enable";

        internal override async Task<string> RunAsync(string[] Args)
        {
            string Alias = Args.ElementAtOrDefault(1) ?? string.Empty;
            bool Enable = IzolabellaConsole.CheckY(this.RequiredName, "Would you like to enable this controller on startup?");
            if (Alias.ToLower(CultureInfo.InvariantCulture) == "all")
            {
                foreach (Controller Controller in IzolabellaOne.KnownControllers)
                {
                    if (Controller.Enabled)
                    {
                        await Controller.StopAsync();
                        await Controller.UpdateProfileAsync(DataStores.ControllerProfileStore, Controller.LastProfile, A => A.ControllerEnabled = Enable);
                    }
                }
                Console.Clear();
                await IzolabellaOne.Main();
                return "New main created.";
            }
            else
            {
                Controller? C = IzolabellaOne.KnownControllers.FirstOrDefault(KC => KC.Name.ToLower(CultureInfo.InvariantCulture) == (Args.ElementAtOrDefault(1) ?? string.Empty).ToLower(CultureInfo.InvariantCulture));
                if (C != null)
                {
                    ControllerProfile? CProfile = (await DataStores.ControllerProfileStore.ReadAllAsync<ControllerProfile>()).FirstOrDefault(CPrf => CPrf.Alias == C.Name);
                    if (CProfile != null)
                    {
                        if (!C.Enabled)
                        {
                            await C.StartAsync(CProfile);
                            await Controller.UpdateProfileAsync(DataStores.ControllerProfileStore, C.LastProfile, A => A.ControllerEnabled = Enable);
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