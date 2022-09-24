// 
// Connect.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using System;
using System.Net;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Networking.Client;
using Fluint.Layer.Networking.Server;
using Fluint.Layer.SDK;

namespace Fluint.SDK.Lambdas.Base;

public class Connect : ILambda
{
    private readonly ModulePacket _packet;

    public Connect(ModulePacket packet)
    {
        _packet = packet;
    }

    public string Command => "connect";

    public LambdaObject Run(string[] args)
    {
        var ip = IPAddress.Loopback.ToString();
        var name = Environment.UserName;
        if (args.Length > 0)
        {
            ip = args[0];
            if (args.Length > 1)
            {
                name = args[1];
            }
        }

        var client = _packet.GetSingleton<IClient>();

        if (client.IsConnected)
        {
            return LambdaObject.Failure;
        }

        if (client.ClientInfo.ID == Guid.Empty)
        {
            client.ClientInfo = new ClientData { Username = name, ID = Guid.NewGuid() };
        }

        client.Connect(new ServerData { IpAddress = ip, Port = 3030, Name = "Fluint Server", TickDelay = 10 });

        return LambdaObject.Success;
    }
}