//
// NcsServer.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Fluint.Layer.Diagnostics;
using Fluint.Layer.Networking;
using Fluint.Layer.Networking.Client;
using NetCoreServer;

namespace Fluint.Networking.Base.Server
{
    internal class NcsServer : UdpServer
    {
        private readonly ILogger _logger;
        private readonly List<NetworkPacket> _packetsToSend = new();
        public readonly Dictionary<EndPoint, ClientData> Clients = new();

        public NcsServer(IPAddress address, int port, ILogger logger) : base(address,
            port)
        {
            _logger = logger;
        }

        protected override void OnReceived(EndPoint endpoint, byte[] buffer, long offset, long size)
        {
            var message = Encoding.UTF8.GetString(buffer, (int)offset, (int)size);
            if (Clients.ContainsKey(endpoint))
            {
                if (message == "bye")
                {
                    _logger.Information("[{0}] {1} said {2} and disconnected.", endpoint, Clients[endpoint].Username,
                        message);
                    Clients.Remove(endpoint);
                    return;
                }

                _logger.Information("[{0}] {1} said: {2}.", endpoint, Clients[endpoint].Username, message);

                foreach (var client in Clients)
                {
                    SendAsync(client.Key, $"[{endpoint}] {client.Value.Username} said: {message}.");
                }
            }
            else
            {
                if (message.StartsWith("hello"))
                {
                    var info = message.Split(';').Skip(1).ToArray();
                    var clientData = new ClientData { ID = new Guid(info[1]), Username = info[0] };

                    if (Clients.Values.All(x => x.ID != clientData.ID))
                    {
                        _logger.Information("[{0}] {2} said {1} and connected.", endpoint, "hello", info[0]);
                        Clients.Add(endpoint, clientData);

                        SendAsync(endpoint, $"welcome {info[1]}:{info[0]}");
                    }
                    else
                    {
                        _logger.Warning("[{0}] {1} is already connected but said hello again!", clientData.ID,
                            clientData.Username);
                    }
                }
            }


            base.OnReceived(endpoint, buffer, offset, size);
        }

        protected override void OnStarted()
        {
            _logger.Information("Live Server started.");
            ReceiveAsync();

            base.OnStarted();
        }

        protected override void OnError(SocketError error)
        {
            _logger.Error("[{0}] The server caught an error with code {1}", Id, error);
            base.OnError(error);
        }

        public void Tick()
        {
            foreach (var client in Clients)
            {
                SendAsync(client.Key, "Tick!");
            }

            ReceiveAsync();
        }
    }
}