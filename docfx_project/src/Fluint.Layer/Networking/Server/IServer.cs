//
// IServer.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;
using System.Collections.Generic;
using Fluint.Layer.Networking.Client;

namespace Fluint.Layer.Networking.Server
{
    /// <summary>
    /// The managing class on the Server-Side.
    /// </summary>
    [Initialization(InitializationMethod.Scoped)]
    public interface IServer : IModule
    {
        /// <summary>
        /// Contains the metadata concerning the server.
        /// </summary>
        IServerData ServerInfo { get; }

        /// <summary>
        /// Contains all the connected users.
        /// </summary>
        IReadOnlyCollection<IClientData> Clients { get; }
        
        /// <summary>
        /// returns true if the server is started, returns false otherwise.
        /// </summary>
        bool ServerStarted { get; }

        /// <summary>
        /// Starts the server.
        /// </summary>
        void Start();

        /// <summary>
        /// Stops the server.
        /// </summary>
        void Stop();

        /// <summary>
        /// Restarts the server.
        /// </summary>
        void Restart();

        /// <summary>
        /// Called when a client connects.
        /// </summary>
        event EventHandler<ClientConnectedEventArgs> ClientConnected;

        /// <summary>
        /// Called when a client disconnects.
        /// </summary>
        event EventHandler<ClientDisconnectedEventArgs> ClientDisconnected;

        /// <summary>
        /// Called when a client sends data.
        /// </summary>
        event EventHandler<ClientSentDataEventArgs> ClientSentData;

    }
}