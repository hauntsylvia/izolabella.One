using izolabella.One.Objects.Constants;
using izolabella.One.Objects.Controllers.Interfaces;
using izolabella.One.Objects.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace izolabella.One.Objects.Controllers.Implementations
{
    internal class KaiaController : IController
    {
        public string Alias => "Kaia";
        public bool Enabled { get; private set; } = false;

        internal KaiaBot? B { get; private set; }

        async Task IController.StartAsync(ControllerProfile Profile)
        {
            this.Enabled = true;
            this.B = new(new(ConfigDefaults.DefaultConfig, true, true, Profile.DiscordBotToken));
            await this.B.Parameters.StartAsync();
        }

        async Task IController.StopAsync()
        {
            this.Enabled = false;
            await (this.B?.Parameters.StopAsync() ?? Task.CompletedTask);
        }
    }
}
