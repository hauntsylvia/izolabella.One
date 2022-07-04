﻿using izolabella.One.Objects.Commands.Inner.Interfaces;
using izolabella.Util;

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

        private readonly List<IIzolabellaConsoleCommand> consoleCommands = BaseImplementationUtil.GetItems<IIzolabellaConsoleCommand>();

        internal IEnumerable<IIzolabellaConsoleCommand> ConsoleCommands => this.consoleCommands.Select<IIzolabellaConsoleCommand, IIzolabellaConsoleCommand>(X => X.WithInitializationAsync(this.consoleCommands.ToArray()).Result);
    }
}
