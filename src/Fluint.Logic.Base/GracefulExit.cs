// 
// GracefulExit.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using System;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Diagnostics;
using Fluint.Layer.Functionality.Common;

namespace Fluint.Logic.Base;

public class GracefulExit : IGracefulExit
{
    private readonly ModulePacket _packet;

    public GracefulExit(ModulePacket packet)
    {
        _packet = packet;
    }

    public void Exit()
    {
        _packet.GetSingleton<ILogger>().Information("Exiting Gracefully, Goodbye!");
        Environment.Exit(0);
    }
}