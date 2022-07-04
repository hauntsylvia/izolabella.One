using izolabella.One.Objects.Commands.Inner.Interfaces;
using izolabella.One.Objects.Controllers.Interfaces;

namespace izolabella.One.Objects.Commands.Inner.Implementations
{
    internal class DisableController : IIzolabellaConsoleCommand
    {
        internal override string RequiredName => "disable";

        internal override async Task<string> RunAsync(string[] Args)
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
