using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace izolabella.One.Objects.Commands.Inner.Interfaces
{
    internal interface IIzolabellaConsoleCommand
    {
        internal string RequiredName { get; }
        internal Task<string> RunAsync(string[] Args);
    }
}
