using izolabella.One.Objects.Commands.Inner.Interfaces;
using izolabella.One.Objects.Constants;
using izolabella.Util.IzolabellaConsole;
using System.Globalization;

namespace izolabella.One.Objects.Commands.Inner.Implementations
{
    internal sealed class  DisableController : IIzolabellaConsoleCommand
    {
        internal override string RequiredName => "disable";

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
                        try
                        {
                            await Controller.StopAsync();
                        }
                        catch
                        {

                        }
                        await Controller.UpdateProfileAsync(DataStores.ControllerProfileStore, Controller.LastProfile, A => A.ControllerEnabled = Enable);
                    }
                }
                return "**  All  ** controllers disabled.";
            }
            else
            {
                Controller? Controller = IzolabellaOne.KnownControllers.FirstOrDefault(KC => KC.Name.ToLower(CultureInfo.InvariantCulture) == Alias.ToLower(CultureInfo.InvariantCulture));
                if (Controller != null)
                {
                    if (Controller.Enabled)
                    {
                        try
                        {
                            await Controller.StopAsync();
                        }
                        catch
                        {

                        }
                        await Controller.UpdateProfileAsync(DataStores.ControllerProfileStore, Controller.LastProfile, A => A.ControllerEnabled = Enable);
                        return "Controller disabled.";
                    }
                    else
                    {
                        return "Controller already disabled.";
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