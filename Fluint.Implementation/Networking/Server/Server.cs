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
using Fluint.Layer.Debugging;
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
            server = new NcsServer(IPAddress.Parse(ServerInfo.IpAddress), ServerInfo.Port, tasks, logger);
            server.ClientConnected += ClientConnected;
            server.ClientDisconnected += ClientDisconnected;
        }

        private readonly NcsServer server;

        private readonly ILogger _logger;
        public IServerData ServerInfo { get; }
        public IReadOnlyCollection<IClientData> Clients => _clients;
        public bool ServerStarted { get; private set; }

        private List<IClientData> _clients = new List<IClientData>();

        public void Start()
        {
            server.Start();
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
            server.Stop();
            ServerStarted = false;
        }

        public void Restart()
        {
            ServerStarted = false;
            server.Restart();
            ServerStarted = true;
            _logger.Information("Server Restarted.");
        }

        private void Tick()
        {
            server.Tick();
        }


        public event EventHandler<ClientConnectedEventArgs> ClientConnected;
        public event EventHandler<ClientDisconnectedEventArgs> ClientDisconnected;
        public event EventHandler<ClientSentDataEventArgs> ClientSentData;
    }
}
