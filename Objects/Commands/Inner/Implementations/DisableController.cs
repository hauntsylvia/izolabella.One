using izolabella.One.Objects.Commands.Inner.Interfaces;
using izolabella.One.Objects.Controllers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace izolabella.One.Objects.Commands.Inner.Implementations
{
    internal class DisableController : IIzolabellaConsoleCommand
    {
        string IIzolabellaConsoleCommand.RequiredName => "disable";

        async Task<string> IIzolabellaConsoleCommand.RunAsync(string[] Args)
        {
            string Alias = Args.ElementAtOrDefault(1) ?? string.Empty;
            if (Alias.ToLower() == "all")
            {
                foreach(IController Controller in Program.KnownControllers)
                {
                    if(Controller.Enabled)
                    {
                        await Controller.StopAsync();
                    }
                }
                return "**  All  ** controllers disabled.";
            }
            else
            {
                IController? Controller = Program.KnownControllers.FirstOrDefault(KC => KC.Alias.ToLower() == Alias.ToLower());
                if (Controller != null)
                {
                    if (Controller.Enabled)
                    {
                        await Controller.StopAsync();
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
