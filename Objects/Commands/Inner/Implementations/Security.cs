using izolabella.Music.Constants;
using izolabella.Music.Structure.ClientData;
using izolabella.One.Objects.Commands.Inner.Interfaces;
using izolabella.Util;

namespace izolabella.One.Objects.Commands.Inner.Implementations
{
    internal sealed class  Security : IIzolabellaConsoleCommand
    {
        internal override string RequiredName => "security";

        internal override bool LowerCase => false;

        internal override async Task<string> RunAsync(string[] Args)
        {
            string Sec = IdGenerator.CreateSecureString();
            await DataStores.SecretsStore.SaveAsync(new Secret(Args.FirstOrDefault() ?? string.Empty, Sec));
            return Sec;

        }
    }
}