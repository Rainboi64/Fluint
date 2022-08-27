// 
// Say.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Networking.Client;
using Fluint.Layer.SDK;

namespace Fluint.SDK.Base.Lambdas;

public class Say : ILambda
{
    private readonly ModulePacket _packet;

    public Say(ModulePacket packet)
    {
        _packet = packet;
    }

    public string Command => "say";

    public LambdaObject Run(string[] args)
    {
        var client = _packet.GetSingleton<IClient>();
        foreach (var message in args)
        {
            client.SendMessage(message);
        }

        return LambdaObject.Success;
    }
}