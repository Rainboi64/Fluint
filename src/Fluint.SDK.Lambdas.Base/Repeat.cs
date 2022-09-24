// 
// Repeat.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using System.Linq;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.SDK;

namespace Fluint.SDK.Lambdas.Base;

public class Repeat : ILambda
{
    private readonly ModulePacket _packet;

    public Repeat(ModulePacket packet)
    {
        _packet = packet;
    }

    public string Command => "repeat";

    public LambdaObject Run(string[] args)
    {
        var length = args.Length;
        if (length < 2)
        {
            return LambdaObject.Error(
                "Invalid arguments, please supply the amount of times the command will be executed in the following format 'repeat {count} {command}'.");
        }

        if (!int.TryParse(args[0], out var count))
        {
            return LambdaObject.Error(
                "Invalid arguments, please supply the amount of times the command will be executed in the following format 'repeat {count} {command}'.");
        }

        var parser = _packet.CreateScoped<ILambdaParser>();

        for (var i = 0; i < count; i++)
        {
            parser.Parse(args[1], args.Skip(2).ToArray());
        }

        return LambdaObject.Success;
    }
}