// 
// InformationCommand.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Diagnostics;
using Fluint.Layer.SDK;

namespace Fluint.SDK.Base.Commands;

public class InformationCommand : ICommand
{
    private readonly ModulePacket _packet;

    public InformationCommand(ModulePacket packet)
    {
        _packet = packet;
    }

    public string Command => "information";

    public void Do(string[] args)
    {
        var logger = _packet.GetSingleton<ILogger>();
        foreach (var message in args)
        {
            logger.Information(message);
        }
    }
}