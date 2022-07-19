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
                ControllerProfile? Profile = Profiles.FirstOrDefault(P => P.Alias == Controller.Name);
                if (Profile == null && Controller.NeedsProfileToken)
                {
                    if (IzolabellaConsole.CheckY(Controller.Name, $"The profile is requesting a token."))
                    {
                        if (IzolabellaConsole.GetProtectedNext(Controller.Name, "Type the token.", out string? Token))
                        {
                            Profile = new(Controller.Name, Token, IzolabellaConsole.CheckY(Controller.Name, "Should this profile be enabled?"));
                            await DataStores.ControllerProfileStore.SaveAsync(Profile);
                        }
                    }
                    else
                    {
                        IzolabellaConsole.Write(Controller.Name, "Skipped.");
                    }
                }

                if ((Profile != null && Profile.ControllerEnabled) || !Controller.NeedsProfileToken)
                {
                    IzolabellaConsole.Write(Controller.Name, "Starting.");
                    try
                    {
                        await Controller.StartAsync(Profile ?? new ControllerProfile(Controller.Name, string.Empty, true)).ConfigureAwait(false);
                        IzolabellaConsole.Write(Controller.Name, "Started.");
                    }
                    catch (Exception Ex)
                    {
                        IzolabellaConsole.Write(Controller.Name, $"There was a problem starting the controller. -> {Ex}");
                    }
                }
            }
            await new Objects.Commands.ConsoleCommandHandler().StartAsync();
        }
    }
}