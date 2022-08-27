// 
// Listen.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using System.Net;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Networking.Server;
using Fluint.Layer.SDK;

namespace Fluint.SDK.Base.Lambdas;

public class Listen : ILambda
{
    private readonly ModulePacket _packet;

    public Listen(ModulePacket packet)
    {
        _packet = packet;
    }

    public string Command => "listen";

    public LambdaObject Run(string[] args)
    {
        var server = _packet.GetSingleton<IServer>();

        var ip = IPAddress.Loopback.ToString();
        if (args.Length > 0)
        {
            ip = args[0];
        }

        server.ServerInfo = new ServerData { IpAddress = ip, Port = 3030, Name = "SDK Server", TickDelay = 10 };
        server.Start();

        return LambdaObject.Success;
    }
}