// 
// Verbose.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Diagnostics;
using Fluint.Layer.SDK;

namespace Fluint.SDK.Base.Lambdas;

public class Verbose : ILambda
{
    private readonly ModulePacket _packet;

    public Verbose(ModulePacket packet)
    {
        _packet = packet;
    }

    public string Command => "verbose";

    public LambdaObject Run(string[] args)
    {
        var logger = _packet.GetSingleton<ILogger>();
        foreach (var message in args)
        {
            logger.Verbose(message);
        }

        return LambdaObject.Success;
    }
}