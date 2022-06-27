using izolabella.One.Objects.Commands.Inner.Interfaces;
using izolabella.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace izolabella.One.Objects.Commands
{
    internal class ConsoleCommandHandler
    {
        internal async Task StartAsync()
        {
            await Task.Run(async () =>
            {
                while (true)
                {
                    if (Program.GetNext("CommandListener", "Awaiting new command.", out string? Res) && Res != null)
                    {
                        string[] Args = Res.Split(' ');
                        IIzolabellaConsoleCommand? Command = this.ConsoleCommands.FirstOrDefault(C => C.RequiredName == (Args.FirstOrDefault() ?? string.Empty));
                        if(Command != null)
                        {
                            Program.Write($"CommandListener/{Command.RequiredName}", await Command.RunAsync(Args));
                        }
                    }
                }
            });
        }

        internal List<IIzolabellaConsoleCommand> ConsoleCommands { get; } = BaseImplementationUtil.GetItems<IIzolabellaConsoleCommand>();
    }
}
