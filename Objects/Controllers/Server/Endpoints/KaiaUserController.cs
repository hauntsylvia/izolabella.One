using izolabella.Backend.Objects.Structures.Controllers.Arguments;
using izolabella.Backend.Objects.Structures.Controllers.Bases;
using izolabella.Backend.Objects.Structures.Controllers.Results;
using Kaia.Bot.Objects.KaiaStructures.Users;
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

        public override Task<IzolabellaAPIControllerResult> RunAsync(IEnumerable<IzolabellaControllerArgument> Arguments)
        {
            return (Arguments.FirstOrDefault()?.TryParse(out ulong Id) ?? false) && Id != default
                ? Task.FromResult<IzolabellaAPIControllerResult>(new(new KaiaUser(Id)))
                : Task.FromResult<IzolabellaAPIControllerResult>(new("die!"));
        }
    }
}
