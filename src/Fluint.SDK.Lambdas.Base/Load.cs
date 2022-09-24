// 
// Load.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using System;
using System.IO;
using System.Threading.Tasks;
using Fluint.Layer;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Diagnostics;
using Fluint.Layer.SDK;

namespace Fluint.SDK.Lambdas.Base;

[Module("Load Command", "Loads and evaluates commands from filestream seperated by semicolons",
    "use this command to load .flntsc files")]
public class Load : ILambda
{
    private readonly ModulePacket _packet;

    public Load(ModulePacket packet)
    {
        _packet = packet;
    }

    public string Command => "load";

    public LambdaObject Run(string[] args)
    {
        var listener = _packet.CreateScoped<ILambdaListener>();

        Parallel.ForEach(args, argument => {
            if (!File.Exists(argument))
            {
                _packet.GetSingleton<ILogger>().Error("[{0}] \"{1}\":Path not found.", "Load", argument);
                return;
            }

            var source = File.ReadAllText(argument);
            var commands = source.Split(";", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            foreach (var command in commands)
            {
                listener.Execute(command);
            }
        });

        return LambdaObject.Success;
    }
}