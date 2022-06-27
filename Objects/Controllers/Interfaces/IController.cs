using izolabella.One.Objects.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
