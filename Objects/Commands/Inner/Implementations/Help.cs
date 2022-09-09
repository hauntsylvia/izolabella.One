using izolabella.One.Objects.Commands.Inner.Interfaces;

namespace izolabella.One.Objects.Commands.Inner.Implementations;

internal class Help : IIzolabellaConsoleCommand
{
    internal override string RequiredName => "help";

    internal override Task<string> RunAsync(string[] Args)
    {
        string A = string.Empty;
        foreach(IIzolabellaConsoleCommand Command in this.Commands)
        {
            A += $"{(Command == this.Commands.First() ? "\n" : string.Empty)}{Command.RequiredName}{(Command != this.Commands.Last() ? "\n" : string.Empty)}";
        }
        return Task.FromResult(A);
    }
}
