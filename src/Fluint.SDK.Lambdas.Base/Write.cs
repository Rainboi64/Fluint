// 
// Write.cs
// 
// Copyright (C) 2022 Yaman Alhalabi

using System.IO;
using System.Linq;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.SDK;

namespace Fluint.SDK.Lambdas.Base;

public class Write : ILambda
{
    private readonly ModulePacket _packet;

    public Write(ModulePacket packet)
    {
        _packet = packet;
    }

    public string Command => "write";

    public LambdaObject Run(string[] args)
    {
        var length = args.Length;
        if (length < 2)
        {
            return LambdaObject.Error(
                "Invalid arguments, please supply the file you want to output to in the following format 'write {filename} {command}'.");
        }

        var parser = _packet.CreateScoped<ILambdaParser>();

        var output = parser.Parse(args[1], args.Skip(2).ToArray());
        File.WriteAllText(args[0], output.Data.ToString());

        return output;
    }
}