﻿//
// ServerTaskHelper.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;
using System.Collections.Generic;
using Fluint.Layer.Networking;
using Fluint.Layer.Networking.Client;
using Fluint.Layer.Networking.Server;

namespace Fluint.Networking.Base.Server
{
    public class ServerTaskHelper : IServerTaskHelper
    {
        public ServerTaskHelper()
        {
            PacketsToMultiCast = new List<NetworkPacket>();
        }

        public List<NetworkPacket> PacketsToMultiCast
        {
            get;
        }

        public void InvokeConnectedEvent(IClientData client)
        {
            ClientConnected?.Invoke(this, new ClientConnectedEventArgs(client));
        }

        public void InvokeDisconnectedEvent(IClientData client, DisconnectionReason reason)
        {
            ClientDisconnected?.Invoke(this, new ClientDisconnectedEventArgs(client, reason));
        }

        public event EventHandler<ClientConnectedEventArgs> ClientConnected;
        public event EventHandler<ClientDisconnectedEventArgs> ClientDisconnected;
    }
}