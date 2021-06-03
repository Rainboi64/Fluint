//
// ConnectedToServerEventArgs.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Fluint.Layer.Networking.Server;

namespace Fluint.Layer.Networking.Client
{
    /// <summary>
    /// event arguments for when the client is connected to the server.
    /// </summary>
    public class ConnectedToServerEventArgs : EventArgs
    {
        public ConnectedToServerEventArgs(IServerData serverInfo, IEnumerable<IClientData> clientsConnected)
        {
            ServerInfo = serverInfo;
            ClientsConnected = (ReadOnlyCollection<IClientData>)clientsConnected;
        }

        /// <summary>
        /// The info of the server connected to.
        /// </summary>
        public IServerData ServerInfo { get; }

        /// <summary>
        /// The Collection of clients who are connected to this server, including this one.
        /// </summary>
        public ReadOnlyCollection<IClientData> ClientsConnected { get; }
    }
}