// 
// Clients.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Diagnostics;
using Fluint.Layer.Networking.Server;
using Fluint.Layer.SDK;

namespace Fluint.SDK.Base.Lambdas;

public class Clients : ILambda
{
    private readonly ILogger _logger;

    private readonly ModulePacket _packet;

    public Clients(ModulePacket packet, ILogger logger)
    {
        _packet = packet;
        _logger = logger;
    }

    public string Command => "clients";

    public LambdaObject Run(string[] args)
    {
        var clients = _packet.GetSingleton<IServer>().Clients;
        return new LambdaObject(clients, LambdaStatus.Success);
    }
}