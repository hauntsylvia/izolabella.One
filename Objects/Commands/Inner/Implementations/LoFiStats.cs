﻿using izolabella.LoFi.Server.Structures.Clients;
using izolabella.One.Objects.Commands.Inner.Interfaces;

namespace izolabella.One.Objects.Commands.Inner.Implementations
{
    internal class LoFiStats : IIzolabellaConsoleCommand
    {
        internal override string RequiredName => "LoFiStats";

        internal override Task<string> RunAsync(string[] Args)
        {
            if (IzolabellaOne.KnownControllers.FirstOrDefault(C => C is IzolabellaLoFiServer) is IzolabellaLoFiServer Server)
            {
                List<string> Display = new()
                {
                    $"\n - {Math.Round(Server.Queue.Sum(Song => Song.FileInformation.FileDuration.TotalMinutes), 2)} minutes of music loaded",
                    $"Currently playing {Server.NowPlaying.Name} by {Server.NowPlaying.AuthorNamesConcat}"
                };
                return Task.FromResult(string.Join("\n - ", Display));
            }
            else
            {
                return Task.FromResult("No server initialized.");
            }
        }
    }
}