using izolabella.One.Objects.Commands.Inner.Interfaces;

namespace izolabella.One.Objects.Commands.Inner.Implementations
{
    internal class Clear : IIzolabellaConsoleCommand
    {
        internal override string RequiredName => "clear";

        internal override Task<string> RunAsync(string[] Args)
        {
            Console.Clear();
            return Task.FromResult("IzolabellaConsole cleared.");
        }
    }
}