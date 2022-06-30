using izolabella.One.Objects.Commands.Inner.Interfaces;

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
