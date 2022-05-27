using izolabella.Storage.Objects.Structures;
using Newtonsoft.Json;

namespace izolabella.One.Objects.Entities
{
    [JsonObject(MemberSerialization.OptIn)]
    public class CommandLog : IDataStoreEntity
    {
        [JsonConstructor]
        public CommandLog(ulong AuthorId)
        {
            this.AuthorId = AuthorId;
        }

        private static DateTime From => new(2000, 1, 1);

        [JsonProperty("AuthorId", Required = Required.Always)]
        public ulong AuthorId { get; }

        public ulong Id => (ulong)(DateTime.Now - From).TotalSeconds;

        public DateTime Time => From + TimeSpan.FromSeconds(this.Id);
    }
}
