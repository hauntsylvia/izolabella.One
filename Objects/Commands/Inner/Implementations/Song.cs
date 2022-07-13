using izolabella.Music.Structure.Music.Artists.Inner;
using izolabella.Music.Structure.Music.Songs;
using izolabella.One.Objects.Commands.Inner.Interfaces;
using izolabella.Util;
using Newtonsoft.Json;

namespace izolabella.One.Objects.Commands.Inner.Implementations
{
    internal class Song : IIzolabellaConsoleCommand
    {
        internal override string RequiredName => "song";

        internal override bool LowerCase => false;

        internal override Task<string> RunAsync(string[] Args)
        {
            return Task.FromResult(JsonConvert.SerializeObject(new Music.Structure.Music.Songs.IzolabellaSong(new("A", new PronounSet[] { new() }, null), "A", new("Alias", "Security"))));
        }
    }
}
