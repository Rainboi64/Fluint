// 
// Jsonize.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using Fluint.Layer.DependencyInjection;
using Fluint.Layer.SDK;
using Newtonsoft.Json;

namespace Fluint.SDK.Base.Lambdas;

public class Jsonize : ILambda
{
    private readonly ModulePacket _packet;

    public Jsonize(ModulePacket packet)
    {
        _packet = packet;
    }

    public string Command => "jsonize";

    public LambdaObject Run(string[] args)
    {
        if (args.Length < 1)
        {
            LambdaObject.Error("please an object to jsonize");
        }

        var memory = _packet.GetSingleton<ILambdaMemory>().AsDictionary();

        var obj = memory[args[0]];
        return new LambdaObject($"{JsonConvert.SerializeObject(obj, Formatting.Indented)}\n");
    }
}