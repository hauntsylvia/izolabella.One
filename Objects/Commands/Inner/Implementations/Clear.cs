using izolabella.One.Objects.Commands.Inner.Interfaces;
using izolabella.One.Objects.Controllers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace izolabella.One.Objects.Commands.Inner.Implementations
{
    internal class Clear : IIzolabellaConsoleCommand
    {
        string IIzolabellaConsoleCommand.RequiredName => "clear";

        Task<string> IIzolabellaConsoleCommand.RunAsync(string[] Args)
        {
            Console.Clear();
            return Task.FromResult("Console cleared.");
        }
    }
}
