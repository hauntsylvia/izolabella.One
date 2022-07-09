namespace izolabella.One.Objects.Constants
{
    internal static class Strings
    {
        internal static class App
        {
            internal static string Name => "CompetitiveCounting.Platform";
            internal static Uri ServerUri => new("http://izolabella.ml:80/");
        }
        internal static class DataStoreNames
        {
            internal static string Startup => "Controller Profiles";
            internal static string Constrainments => "Constrainments";
        }
        internal static class Responses
        {
            internal static string CommandConstrained => "oh no! looks like u can't do that. here's what's missing:";
        }
    }
}
