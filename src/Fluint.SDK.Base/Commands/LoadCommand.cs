// 
// LoadCommand.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using System;
using System.IO;
using System.Threading.Tasks;
using Fluint.Layer;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Diagnostics;
using Fluint.Layer.SDK;

namespace Fluint.SDK.Base.Commands;

[Module("Load Command", "Loads and evaluates commands from filestream seperated by semicolons",
    "use this command to load .flntsc files")]
public class LoadCommand : ICommand
{
    private readonly ModulePacket _packet;

    public LoadCommand(ModulePacket packet)
    {
        _packet = packet;
    }

    public string Command => "load";

    public void Do(string[] args)
    {
        var parser = _packet.CreateScoped<IParser>();

        Parallel.ForEach(args, argument => {
            if (!File.Exists(argument))
            {
                _packet.GetSingleton<ILogger>().Error("[{0}] \"{1}\":Path not found.", "LoadCommand", argument);
                return;
            }

            var source = File.ReadAllText(argument);
            var commands = source.Split(";", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            foreach (var command in commands)
            {
                var (identifier, arguments) = CommandLineListener.Parse(command);
                parser.Parse(identifier, arguments);
            }
        });
    }
}