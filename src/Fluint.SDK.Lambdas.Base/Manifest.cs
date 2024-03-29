// 
// Manifest.cs
// 
// Copyright (C) 2022 Yaman Alhalabi

using Fluint.Layer.DependencyInjection;
using Fluint.Layer.SDK;

namespace Fluint.SDK.Lambdas.Base;

public class Manifest : ILambda
{
    private readonly ModulePacket _packet;

    public Manifest(ModulePacket packet)
    {
        _packet = packet;
    }

    public string Command => "manifest";

    public LambdaObject Run(string[] args)
    {
        return new LambdaObject(_packet.CurrentRuntime.ModuleManifest.ToString(), LambdaStatus.Success);
    }
}