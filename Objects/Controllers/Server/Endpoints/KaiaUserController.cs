using izolabella.Backend.Objects.Structures.Controllers.Arguments;
using izolabella.Backend.Objects.Structures.Controllers.Bases;
using izolabella.Backend.Objects.Structures.Controllers.Results;
using izolabella.Kaia.Bot.Objects.KaiaStructures.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace izolabella.One.Objects.Controllers.Server.Endpoints
{
    public class KaiaUserController : IzolabellaController
    {
        public override string Route => "KaiaUser";

        public override Task<IzolabellaAPIControllerResult> RunAsync(IzolabellaControllerArgument Arguments)
        {
            return Arguments.TryParse(out ulong Id) && Id != default
                ? Task.FromResult<IzolabellaAPIControllerResult>(new(new KaiaUser(Id)))
                : Task.FromResult<IzolabellaAPIControllerResult>(new("die!"));
        }
    }
}
