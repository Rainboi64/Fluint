// 
// Warning.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Diagnostics;
using Fluint.Layer.SDK;

namespace Fluint.SDK.Lambdas.Base;

public class Warning : ILambda
{
    private readonly ModulePacket _packet;

    public Warning(ModulePacket packet)
    {
        _packet = packet;
    }

    public string Command => "warning";

    public LambdaObject Run(string[] args)
    {
        var logger = _packet.GetSingleton<ILogger>();
        foreach (var message in args)
        {
            logger.Warning(message);
        }

        return LambdaObject.Success;
    }
}