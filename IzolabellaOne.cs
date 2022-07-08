global using Kaia.Bot.Objects.Clients;
using izolabella.One.Objects.Constants;
using izolabella.Util;
using izolabella.Util.Controllers;
using izolabella.Util.Controllers.Profiles;
using izolabella.Util.IzolabellaConsole;

namespace izolabella.One
{
    public class IzolabellaOne
    {
        internal static async Task Main()
        {
            try
            {
                List<ControllerProfile> Profiles = await DataStores.ControllerProfileStore.ReadAllAsync<ControllerProfile>();
                foreach (Controller Controller in KnownControllers)
                {
                    ControllerProfile? Profile = Profiles.FirstOrDefault(P => P.Alias == Controller.Alias);
                    if (Profile == null)
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

                    if (Profile != null && Profile.ControllerEnabled)
                    {
                        IzolabellaConsole.Write(Controller.Alias, "Starting.");
                        await Controller.StartAsync(Profile);
                        IzolabellaConsole.Write(Controller.Alias, "Started.");
                    }
                }
                await new Objects.Commands.ConsoleCommandHandler().StartAsync();
            }
            catch(Exception Ex)
            {
                IzolabellaConsole.Write("Main", Ex.ToString());
            }
            await Task.Delay(-1);
        }

        internal static List<Controller> KnownControllers { get; } = BaseImplementationUtil.GetItems<Controller>();

    }
}