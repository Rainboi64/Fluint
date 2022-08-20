//
// Exit.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;
using System.Linq;
using System.Threading;
using Fluint.Layer;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Runtime;
using Fluint.Layer.SDK;

namespace Fluint.SDK.Base.Lambdas;

[Module("Adopt Command", "Starts a fluint instance from an SDK", "enter this command to start the application")]
public class Adopt : ILambda
{
    private readonly ModulePacket _packet;

    public Adopt(ModulePacket packet)
    {
        _packet = packet;
    }

    public string Command => "adopt";

    public LambdaObject Run(string[] args)
    {
        var exists = _packet
            .CurrentRuntime
            .Parent.Instances
            .Values
            .OfType<FluintInstance>()
            .Any();

        if (exists)
        {
            Console.WriteLine();
            return LambdaObject.Error("Fluint instance already exists");
        }

        var id = _packet.CurrentRuntime.Parent.CreateInstance<FluintInstance>();
        new Thread(() => {
            _packet.CurrentRuntime.Parent.Start(id);
        }).Start();

        return new LambdaObject("{id}\n");
    }
}