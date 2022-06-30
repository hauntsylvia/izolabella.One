using izolabella.One.Objects.Profiles;

namespace izolabella.One.Objects.Controllers.Interfaces
{
    internal interface IController
    {
        string Alias { get; }

        bool Enabled { get; }

        Task StartAsync(ControllerProfile Profile);

        Task StopAsync();
    }
}
