// 
// Read.cs
// 
// Copyright (C) 2022 Yaman Alhalabi

using System;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.SDK;
using Fluint.Layer.VirtualFileSystem;

namespace Fluint.SDK.Lambdas.Base;

public class Read : ILambda
{
    private readonly ModulePacket _packet;

    public Read(ModulePacket packet)
    {
        _packet = packet;
    }

    public string Command => "read";

    public LambdaObject Run(string[] args)
    {
        var vfs = _packet.CreateScoped<IVirtualFileSystem>();
        vfs.LoadFromFile(args[0]);
        Console.WriteLine(vfs.Root.ToString());
        return new LambdaObject(LambdaStatus.Success);
    }
}