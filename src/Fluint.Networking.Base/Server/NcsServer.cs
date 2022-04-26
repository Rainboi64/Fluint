//
// NcsServer.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

/**
 * @author Yaman Alhalabi <yamanalhalabi2@gmail.com>
 * @file inner server using NetCoreServer
 * @desc Created on 2020-12-11 7:50:07 pm
 * @copyright Panic Factory (C) 2020 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using Fluint.Layer.Diagnostics;
using Fluint.Layer.Networking;
using Fluint.Layer.Networking.Server;
using NetCoreServer;
using Newtonsoft.Json;

namespace Fluint.Networking.Base.Server
{
    internal class NcsServer : TcpServer
    {
        // public event EventHandler<ClientSentDataEventArgs> ClientSentData;

        private readonly ILogger _logger;
        private readonly List<NetworkPacket> _packetsToSend = new List<NetworkPacket>();
        private readonly IEnumerable<IServerTask> _tasks;

        public NcsServer(IPAddress address, int port, IEnumerable<IServerTask> tasks, ILogger logger) : base(address,
            port)
        {
            _logger = logger;
            _tasks = tasks;
        }

        public event EventHandler<ClientConnectedEventArgs> ClientConnected;
        public event EventHandler<ClientDisconnectedEventArgs> ClientDisconnected;

        protected override TcpSession CreateSession()
        {
            return new ServerTcpSession(this);
        }

        protected override void OnConnected(TcpSession session)
        {
            _logger.Error($"[INTERNAL] Connection established with client GUID #{session.Id}.");
            base.OnConnected(session);
        }

        protected override void OnStarted()
        {
            _logger.Error($"[INTERNAL] Internal Server with GUID #{this.Id} started.");
            base.OnStarted();
        }

        protected override void OnError(SocketError error)
        {
            _logger.Error($"[INTERNAL] The server caught an error with code {error}");
        }

        public void Tick()
        {
            foreach (var sessionPair in Sessions)
            {
                var session = (ServerTcpSession)sessionPair.Value;
                foreach (var packet in session.PacketsReceived)
                {
                    foreach (var task in _tasks.Where(x => x.TaskId == packet.Task))
                    {
                        var taskHelper = new ServerTaskHelper();
                        taskHelper.ClientConnected += ClientConnected;
                        taskHelper.ClientDisconnected += ClientDisconnected;
                        IServerTaskHelper iTaskHelper = taskHelper;
                        task.Do(ref iTaskHelper);
                        _packetsToSend.AddRange(iTaskHelper.PacketsToMultiCast);
                    }
                }

                foreach (var packet in _packetsToSend)
                {
                    Multicast(JsonConvert.SerializeObject(packet));
                }
            }
        }
    }
}