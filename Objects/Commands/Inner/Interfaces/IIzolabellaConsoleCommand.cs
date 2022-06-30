namespace izolabella.One.Objects.Commands.Inner.Interfaces
{
    internal interface IIzolabellaConsoleCommand
    {
        internal string RequiredName { get; }
        internal Task<string> RunAsync(string[] Args);
    }
}
