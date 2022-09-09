using izolabella.Backend.Objects.Structures.Controllers.Arguments;
using izolabella.Backend.Objects.Structures.Controllers.Bases;
using izolabella.Backend.Objects.Structures.Controllers.Results;
using izolabella.Kaia.Bot.Objects.KaiaStructures.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace izolabella.One.Objects.Controllers.Server.Endpoints;

public class PingController : IzolabellaEndpoint
{
    public override string Route => "Ping";

    public override Task<IzolabellaAPIControllerResult> RunAsync(IzolabellaControllerArgument Arguments)
    {
        return Task.FromResult<IzolabellaAPIControllerResult>(new("I am alive! Are you?"));
    }
}
