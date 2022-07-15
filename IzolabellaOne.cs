global using izolabella.Kaia.Bot.Objects.Clients;
using System.Reflection;
using izolabella.LoFi.Server.Structures.Clients;
using izolabella.One.Objects.Constants;
using izolabella.Util;
using izolabella.Util.Controllers;
using izolabella.Util.Controllers.Profiles;
using izolabella.Util.IzolabellaConsole;

namespace izolabella.One
{
    public class IzolabellaOne
    {
        public static IEnumerable<Controller> KnownControllers { get; } = Controller.GetControllers(new Assembly?[] { Assembly.GetAssembly(typeof(IzolabellaOne)), Assembly.GetAssembly(typeof(IzolabellaLoFiServer)) });

        internal static async Task Main()
        {
            await StartControllersAsync();
            await Task.Delay(-1);
        }

        internal static async Task StartControllersAsync()
        {
            List<ControllerProfile> Profiles = await DataStores.ControllerProfileStore.ReadAllAsync<ControllerProfile>();
            foreach (Controller Controller in KnownControllers)
            {
                ControllerProfile? Profile = Profiles.FirstOrDefault(P => P.Alias == Controller.Alias);
                if (Profile == null && Controller.NeedsProfileToken)
                {
                    if (IzolabellaConsole.CheckY(Controller.Alias, $"The profile is requesting a token."))
                    {
                        if (IzolabellaConsole.GetProtectedNext(Controller.Alias, "Type the token.", out string? Token))
                        {
                            Profile = new(Controller.Alias, Token, IzolabellaConsole.CheckY(Controller.Alias, "Should this profile be enabled?"));
                            await DataStores.ControllerProfileStore.SaveAsync(Profile);
                        }
                    }
                    else
                    {
                        IzolabellaConsole.Write(Controller.Alias, "Skipped.");
                    }
                }

                if ((Profile != null && Profile.ControllerEnabled) || !Controller.NeedsProfileToken)
                {
                    IzolabellaConsole.Write(Controller.Alias, "Starting.");
                    try
                    {
                        await Controller.StartAsync(Profile ?? new ControllerProfile(Controller.Alias, string.Empty, true)).ConfigureAwait(false);
                        IzolabellaConsole.Write(Controller.Alias, "Started.");
                    }
                    catch (Exception Ex)
                    {
                        IzolabellaConsole.Write(Controller.Alias, $"There was a problem starting the controller. -> {Ex}");
                    }
                }
            }
            await new Objects.Commands.ConsoleCommandHandler().StartAsync();
        }
    }
}