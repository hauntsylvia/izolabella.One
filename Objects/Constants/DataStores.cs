using izolabella.Storage.Objects.DataStores;

namespace izolabella.One.Objects.Constants
{
    internal static class DataStores
    {
        internal static DataStore ControllerProfileStore => new(Strings.App.Name, Strings.DataStoreNames.Startup);
        internal static DataStore ConstrainmentStore => new(Strings.App.Name, Strings.DataStoreNames.Constrainments);
        internal static DataStore ExceptionsStore => new(Strings.App.Name, Strings.DataStoreNames.Exceptions);
    }
}
