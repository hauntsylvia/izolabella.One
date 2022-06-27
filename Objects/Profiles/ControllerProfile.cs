﻿using izolabella.Storage.Objects.Structures;
using izolabella.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace izolabella.One.Objects.Profiles
{
    public class ControllerProfile : IDataStoreEntity 
    {
        public ControllerProfile(string Alias, string DiscordBotToken, bool ControllerEnabled, ulong? Id = null)
        {
            this.Alias = Alias;
            this.DiscordBotToken = DiscordBotToken;
            this.ControllerEnabled = ControllerEnabled;
            this.Id = Id ?? IdGenerator.CreateNewId();
        }

        /// <summary>
        /// The alias of this profile. Must match the alias of the controller to be passed.
        /// </summary>
        public string Alias { get; }

        public string DiscordBotToken { get; }

        /// <summary>
        /// Whether the profile should be enabled.
        /// </summary>
        public bool ControllerEnabled { get; }

        public ulong Id { get; }
    }
}
