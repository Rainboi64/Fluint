// 
// Create.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using Fluint.Layer.DependencyInjection;
using Fluint.Layer.SDK;

namespace Fluint.SDK.Lambdas.Base;

public class Create : ILambda
{
    private readonly ModulePacket _packet;

    public Create(ModulePacket packet)
    {
        _packet = packet;
    }

    public string Command => "create";

    public LambdaObject Run(string[] args)
    {
        if (args.Length < 2)
        {
            return LambdaObject.Error("missing argument(s) for creating an object");
        }

        var memory = _packet.GetSingleton<ILambdaMemory>();

        object result = null;
        var found = false;

        foreach (var type in _packet.ScopedMappings.Keys)
        {
            if (type.FullName != args[0])
            {
                continue;
            }

            found = true;
            result = _packet.FetchAndCreateInstance(_packet.ScopedMappings[type]);
            break;
        }

        foreach (var type in _packet.SingletonMappings.Keys)
        {
            if (type.FullName != args[0])
            {
                continue;
            }

            found = true;
            result = _packet.SingletonMappings[type];
            break;
        }

        if (!found)
        {
            return LambdaObject.Error("Couldn't find a scoped/singleton with that name.");
        }

        memory.Add(args[1], result);

        return new LambdaObject(result);
    }
}