// 
// Dump.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using System.Text;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.SDK;

namespace Fluint.SDK.Base.Lambdas;

public class Dump : ILambda
{
    private readonly ModulePacket _packet;

    public Dump(ModulePacket packet)
    {
        _packet = packet;
    }

    public string Command => "dump";

    public LambdaObject Run(string[] args)
    {
        var memory = _packet.GetSingleton<ILambdaMemory>().AsDictionary();

        var builder = new StringBuilder();

        foreach (var (name, obj) in memory)
        {
            builder.AppendLine($"{name} => {obj}");
        }

        builder.AppendLine($"Count: {memory.Count}");

        return new LambdaObject(builder.ToString());
    }
}