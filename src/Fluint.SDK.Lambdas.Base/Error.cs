// 
// Error.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Diagnostics;
using Fluint.Layer.SDK;

namespace Fluint.SDK.Lambdas.Base;

public class Error : ILambda
{
    private readonly ModulePacket _packet;

    public Error(ModulePacket packet)
    {
        _packet = packet;
    }

    public string Command => "error";

    public LambdaObject Run(string[] args)
    {
        var logger = _packet.GetSingleton<ILogger>();
        foreach (var message in args)
        {
            logger.Error(message);
        }

        return LambdaObject.Success;
        ;
    }
}