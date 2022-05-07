// 
// Singleton.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using System;
using System.Linq;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.SDK;

namespace Fluint.SDK.Base.Lambdas;

public class Singleton : ILambda
{
    private readonly ModulePacket _packet;

    public Singleton(ModulePacket packet)
    {
        _packet = packet;
    }

    public string Command => "singleton";

    public LambdaObject Run(string[] args)
    {
        if (args.Length <= 2)
        {
            return LambdaObject.Error("Missing argument for using a singleton");
        }

        Type result = null;

        foreach (var type in _packet.SingletonMappings.Keys)
        {
            if (type.FullName == args[0])
            {
                result = type;
                break;
            }
        }

        if (result is null)
        {
            return LambdaObject.Error($"Singleton with name {args[0]} not found.");
        }

        var memory = _packet.GetSingleton<ILambdaMemory>();
        var singleton = _packet.SingletonMappings[result];

        switch (args[1].ToLower())
        {
            case "field":

            case "property":
                return new LambdaObject(GetParameter(args[2], result));
                break;

            case "func":
                var parameters = args.Skip(3).ToArray();

                var obj = InvokeMethod(args[2], result, parameters);
                return obj is not null ? new LambdaObject(obj) : LambdaObject.Unknown;

            default:
                return LambdaObject.Error(
                    $"Invalid operation [{args[1]}] on {args[0]}. use 'help singleton' for valid operations");
        }
    }

    private object GetParameter(string name, Type obj)
    {
        var singleton = _packet.SingletonMappings[obj];

        var value = obj
            .GetProperties()
            .FirstOrDefault(info => info.Name == name)
            ?.GetValue(singleton);

        return value;
    }

    private object InvokeMethod(string funcName, Type obj, object[] parameters)
    {
        var singleton = _packet.SingletonMappings[obj];

        var method = singleton
            .GetType()
            .GetMethods()
            .Where(info => info.Name == funcName)
            .FirstOrDefault(info => info.GetParameters().Length == parameters.Length);

        if (method is not null)
        {
            return method.Invoke(singleton, parameters);
        }

        Console.WriteLine($"Invalid method with name [{funcName[1]}] on {obj} and with specified parameters.");
        return null;
    }
}