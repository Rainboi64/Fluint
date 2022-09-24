// 
// Fatal.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Diagnostics;
using Fluint.Layer.SDK;

namespace Fluint.SDK.Lambdas.Base;

public class Fatal : ILambda
{
    private readonly ModulePacket _packet;

    public Fatal(ModulePacket packet)
    {
        _packet = packet;
    }

    public string Command => "fatal";

    public LambdaObject Run(string[] args)
    {
        var logger = _packet.GetSingleton<ILogger>();
        foreach (var message in args)
        {
            logger.Fatal(message);
        }

        return LambdaObject.Success;
        ;
    }
}