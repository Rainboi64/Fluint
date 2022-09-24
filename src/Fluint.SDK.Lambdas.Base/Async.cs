// 
// Async.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using System.Linq;
using System.Threading;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.SDK;

namespace Fluint.SDK.Lambdas.Base;

public class Async : ILambda
{
    private readonly ModulePacket _packet;

    public Async(ModulePacket packet)
    {
        _packet = packet;
    }

    public string Command => "async";

    public LambdaObject Run(string[] args)
    {
        var parser = _packet.CreateScoped<ILambdaParser>();

        new Thread(() => {
            parser.Parse(args[0], args.Skip(1).ToArray());
        }).Start();

        return LambdaObject.Success;
    }
}