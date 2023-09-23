using System.Globalization;
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

        internal override async Task<string> RunAsync(string[] Args)
        {
            if (ulong.TryParse(Args.ElementAtOrDefault(1), out ulong Id)
                && File.Exists(Path.Combine(izolabella.LoFi.Server.Structures.Constants.DataStores.MusicFilesStore.Location.FullName, $"{Id.ToString(CultureInfo.InvariantCulture)}.wav")))
            {
                IzolabellaSong? Song = await izolabella.LoFi.Server.Structures.Constants.DataStores.SongStore.ReadAsync<IzolabellaSong>(Id);
                return Song != null ? JsonConvert.SerializeObject(Song) : "Song not found.";
            }
            else
            {
                return IdGenerator.CreateNewId().ToString(CultureInfo.InvariantCulture);
            }
        }
    }
}