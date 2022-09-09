namespace izolabella.One.Objects.Commands.Inner.Interfaces;

internal abstract class IIzolabellaConsoleCommand
{
    internal abstract string RequiredName { get; }

    internal virtual bool LowerCase { get; } = true;

    internal abstract Task<string> RunAsync(string[] Args);

    internal IIzolabellaConsoleCommand[] Commands { get; private set; } = Array.Empty<IIzolabellaConsoleCommand>();
    
    internal Task<IIzolabellaConsoleCommand> WithInitializationAsync(IIzolabellaConsoleCommand[] Commands)
    {
        this.Commands = Commands;
        return Task.FromResult(this);
    }
}
