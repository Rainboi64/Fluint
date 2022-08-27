// 
// Disconnect.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Networking.Client;
using Fluint.Layer.SDK;

namespace Fluint.SDK.Base.Lambdas;

public class Disconnect : ILambda
{
    private readonly ModulePacket _packet;

    public Disconnect(ModulePacket packet)
    {
        _packet = packet;
    }

    public string Command => "disconnect";

    public LambdaObject Run(string[] args)
    {
        var client = _packet.GetSingleton<IClient>();
        client.Disconnect();

        return LambdaObject.Success;
    }
}