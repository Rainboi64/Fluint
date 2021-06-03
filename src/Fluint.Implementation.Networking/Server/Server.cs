//
// Server.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

/**
 * @author Yaman Alhalabi <yamanalhalabi2@gmail.com>
 * @file The standard implementation for the server class in Fluint.Layer
 * @desc Created on 2020-12-11 7:50:10 pm
 * @copyright Panic Factory (C) 2020 
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Fluint.Implementation.Networking.Client;
using Fluint.Layer.Diagnostics;
using Fluint.Layer.Networking;
using Fluint.Layer.Networking.Client;
using Fluint.Layer.Networking.Server;
using NetCoreServer;
using Newtonsoft.Json;

namespace Fluint.Implementation.Networking.Server
{
    public class Server : IServer
    {
        public Server(IServerData serverInfo, IEnumerable<IServerTask> tasks, ILogger logger)
        {
            _logger = logger;
            ServerInfo = serverInfo;
            _server = new NcsServer(IPAddress.Parse(ServerInfo.IpAddress), ServerInfo.Port, tasks, logger);
            _server.ClientConnected += ClientConnected;
            _server.ClientDisconnected += ClientDisconnected;
        }

        private readonly NcsServer _server;

        private readonly ILogger _logger;
        public IServerData ServerInfo { get; }
        public IReadOnlyCollection<IClientData> Clients => _clients;
        public bool ServerStarted { get; private set; }

        private readonly List<IClientData> _clients = new();

        public void Start()
        {
            _server.Start();
            _logger.Information("Server Started.");
            ServerStarted = true;
            while (ServerStarted)
            {
                Thread.Sleep(ServerInfo.TickDelay);
                Tick();
            }
        }

        public void Stop()
        {
            _logger.Information("Server Stopped.");
            _server.Stop();
            ServerStarted = false;
        }

        public void Restart()
        {
            ServerStarted = false;
            _server.Restart();
            ServerStarted = true;
            _logger.Information("Server Restarted.");
        }

        private void Tick()
        {
            _server.Tick();
        }


        public event EventHandler<ClientConnectedEventArgs> ClientConnected;
        public event EventHandler<ClientDisconnectedEventArgs> ClientDisconnected;
        public event EventHandler<ClientSentDataEventArgs> ClientSentData;
    }
}
