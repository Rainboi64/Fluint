// 
// Rebuild.cs
// 
// Copyright (C) 2022 Yaman Alhalabi

using System.IO;
using System.Text.Json;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.SDK;

namespace Fluint.SDK.Lambdas.Base;

public class Rebuild : ILambda
{
    public string Command => "rebuild";

    public LambdaObject Run(string[] args)
    {
        var directory = "base";
        var manifest = "moduleManifest.json";

        switch (args.Length)
        {
            case 1:
                directory = args[0];
                break;
            case 2:
                directory = args[0];
                manifest = args[1];
                break;
        }

        var json = JsonSerializer.Serialize(ModulesManager.GenerateManifest(directory),
            new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(manifest, json);

        return LambdaObject.Success;
    }
}