// 
// Debug.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Diagnostics;
using Fluint.Layer.SDK;

namespace Fluint.SDK.Lambdas.Base;

public class Debug : ILambda
{
    private readonly ModulePacket _packet;

    public Debug(ModulePacket packet)
    {
        _packet = packet;
    }

    public string Command => "debug";

    public LambdaObject Run(string[] args)
    {
        var logger = _packet.GetSingleton<ILogger>();
        foreach (var message in args)
        {
            logger.Information(message);
        }

        return LambdaObject.Success;
    }
}