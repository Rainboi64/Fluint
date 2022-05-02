// 
// FatalCommand.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Diagnostics;
using Fluint.Layer.SDK;

namespace Fluint.SDK.Base.Commands;

public class FatalCommand : ICommand
{
    private readonly ModulePacket _packet;

    public FatalCommand(ModulePacket packet)
    {
        _packet = packet;
    }

    public string Command => "fatal";

    public void Do(string[] args)
    {
        var logger = _packet.GetSingleton<ILogger>();
        foreach (var message in args)
        {
            logger.Fatal(message);
        }
    }
}