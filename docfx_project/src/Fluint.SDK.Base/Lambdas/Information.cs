// 
// Information.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Diagnostics;
using Fluint.Layer.SDK;

namespace Fluint.SDK.Base.Lambdas;

public class Information : ILambda
{
    private readonly ModulePacket _packet;

    public Information(ModulePacket packet)
    {
        _packet = packet;
    }

    public string Command => "information";

    public LambdaObject Run(string[] args)
    {
        var logger = _packet.GetSingleton<ILogger>();
        foreach (var message in args)
        {
            logger.Information(message);
        }

        return LambdaObject.Success;
        ;
    }
}