//
// Server.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System.Collections.Generic;
using System.Net;
using System.Threading;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Diagnostics;
using Fluint.Layer.Networking.Client;
using Fluint.Layer.Networking.Server;

namespace Fluint.Networking.Base.Server;

public class Server : IServer
{
    private readonly ModulePacket _packet;
    private ILogger _logger;

    private NcsServer _server;
    private ServerData _serverInfo;

    public Server(ModulePacket packet)
    {
        _packet = packet;
    }

    public ServerData ServerInfo
    {
        get => _serverInfo;
        set
        {
            _logger = _packet.GetSingleton<ILogger>();

            _server = new NcsServer(IPAddress.Parse(value.IpAddress), value.Port, _logger);
            _serverInfo = value;
        }
    }

    public IReadOnlyCollection<ClientData> Clients => _server.Clients.Values;

    public bool ServerStarted
    {
        get;
        private set;
    }

    public void Start()
    {
        _server.Start(IPAddress.Parse(ServerInfo.IpAddress), ServerInfo.Port);
        ServerStarted = true;
        while (ServerStarted)
        {
            Thread.Sleep(ServerInfo.TickDelay);
            Tick();
        }
    }

    public void Stop()
    {
        _logger.Information("Fluint Server Stopped.");
        _server.Stop();
        ServerStarted = false;
    }

    public void Restart()
    {
        ServerStarted = false;
        _server.Restart();
        ServerStarted = true;
    }

    private void Tick()
    {
        _server.Tick();
    }
}