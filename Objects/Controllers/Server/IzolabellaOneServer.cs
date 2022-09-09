using izolabella.Backend.Objects.Listeners;
using izolabella.LoFi.Server.Structures.Clients;
using izolabella.One.Objects.Constants;
using izolabella.One.Objects.Controllers.Server.Endpoints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace izolabella.One.Objects.Controllers.Server;

public class IzolabellaOneServer : Controller
{
    public IzolabellaServer? Listener { get; private set; }

    public override string Name => "Izolabella.One";

    public override bool NeedsProfileToken => false;

    protected override async Task StartProtectedAsync(ControllerProfile Profile)
    {
        this.Listener = new(Prefixes: Strings.App.KaiaUris, Self: this, AssembliesToLoadFrom: new[] { Assembly.GetAssembly(typeof(KaiaUserController)) });
        await this.Listener.StartListeningAsync();
    }

    protected override async Task StopProtectedAsync()
    {
        await (this.Listener?.StopListeningAsync() ?? Task.CompletedTask);
    }
}
