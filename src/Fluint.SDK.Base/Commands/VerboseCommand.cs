// 
// VerboseCommand.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Diagnostics;
using Fluint.Layer.SDK;

namespace Fluint.SDK.Base.Commands;

public class VerboseCommand : ICommand
{
    private readonly ModulePacket _packet;

    public VerboseCommand(ModulePacket packet)
    {
        _packet = packet;
    }

    public string Command => "verbose";

    public void Do(string[] args)
    {
        var logger = _packet.GetSingleton<ILogger>();
        foreach (var message in args)
        {
            logger.Verbose(message);
        }
    }
}