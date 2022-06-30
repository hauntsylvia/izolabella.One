global using Kaia.Bot.Objects.Clients;
using izolabella.One.Objects.Constants;
using izolabella.One.Objects.Controllers.Interfaces;
using izolabella.One.Objects.Profiles;
using izolabella.Util;

namespace izolabella.One
{
    internal class Program
    {
        internal static async Task Main()
        {
            try
            {
                List<ControllerProfile> Profiles = await DataStores.ControllerProfileStore.ReadAllAsync<ControllerProfile>();
                foreach (IController Controller in KnownControllers)
                {
                    ControllerProfile? Profile = Profiles.FirstOrDefault(P => P.Alias == Controller.Alias);
                    if (Profile == null)
                    {
                        if (CheckY(Controller.Alias, $"The profile is requesting a token."))
                        {
                            if (GetProtectedNext(Controller.Alias, "Type the token.", out string? Token))
                            {
                                Profile = new(Controller.Alias, Token, CheckY(Controller.Alias, "Should this profile be enabled?"));
                                await DataStores.ControllerProfileStore.SaveAsync(Profile);
                            }
                        }
                        else
                        {
                            Write(Controller.Alias, "Skipped.");
                        }
                    }

                    if (Profile != null)
                    {
                        Write(Controller.Alias, "Starting.");
                        await Controller.StartAsync(Profile);
                        Write(Controller.Alias, "Started.");
                    }
                }
                await new Objects.Commands.ConsoleCommandHandler().StartAsync();
            }
            catch(Exception Ex)
            {
                Console.WriteLine(Ex);
            }
            await Task.Delay(-1);
        }

        internal static List<IController> KnownControllers { get; } = BaseImplementationUtil.GetItems<IController>();

        internal static bool CheckY(string Context, string MessageBefore)
        {
            Console.WriteLine($"[{Context}]: {MessageBefore} - Press Y to accept, anything else to decline.");
            return Console.ReadKey(true).Key == ConsoleKey.Y;
        }

        internal static bool GetNext(string Context, string MessageBefore, out string? Result)
        {
            Console.WriteLine($"[{Context}]: {MessageBefore}");
            Result = Console.ReadLine();
            return true;
        }

        internal static bool GetProtectedNext(string Context, string MessageBefore, out string Result)
        {
            Console.WriteLine($"[{Context}]: {MessageBefore}");
            bool R = true;
            Result = string.Empty;
            while(R)
            {
                ConsoleKeyInfo Key = Console.ReadKey(true);
                if(Key.Key == ConsoleKey.Enter)
                {
                    break;
                }
                else
                {
                    Result += Key.KeyChar;
                }
            }
            return true;
        }

        internal static void Write(string Context, string Message)
        {
            Console.WriteLine($"[{Context}]: {Message}");
        }
    }
}