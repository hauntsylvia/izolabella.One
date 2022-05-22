using izolabella.Storage.Objects.DataStores;

namespace izolabella.CompetitiveCounting.Platform.Objects.Constants
{
    internal static class DataStores
    {
        internal static DataStore StartupStore => new(Strings.App.Name, Strings.DataStoreNames.Startup);
    }
}
