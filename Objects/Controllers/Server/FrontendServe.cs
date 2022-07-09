using izolabella.Backend.REST.Objects.Listeners;
using izolabella.One.Objects.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace izolabella.One.Objects.Controllers.Server
{
    public class FrontendServe : Controller
    {
        public IzolabellaServer? Listener { get; private set; }

        public override string Alias => "Frontend Serve";

        public override bool NeedsProfileToken => false;

        protected override Task StartProtectedAsync(ControllerProfile Profile)
        {
            this.Listener = new(Strings.App.ServerUri, this);
            new Thread(async () => await this.Listener.StartListeningAsync()).Start();
            return Task.CompletedTask;
        }

        protected override async Task StopProtectedAsync()
        {
            await (this.Listener?.StopListening() ?? Task.CompletedTask);
        }
    }
}
