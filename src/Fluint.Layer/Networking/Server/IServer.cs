//
// IServer.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System.Collections.Generic;
using Fluint.Layer.Networking.Client;

namespace Fluint.Layer.Networking.Server
{
    /// <summary>
    /// The managing class on the Server-Side.
    /// </summary>
    [Initialization(InitializationMethod.Singleton)]
    public interface IServer : IModule
    {
        /// <summary>
        /// Contains the metadata concerning the server.
        /// </summary>
        ServerData ServerInfo
        {
            get;
            set;
        }

        /// <summary>
        /// Contains all the connected users.
        /// </summary>
        IReadOnlyCollection<ClientData> Clients
        {
            get;
        }

        /// <summary>
        /// returns true if the server is started, returns false otherwise.
        /// </summary>
        bool ServerStarted
        {
            get;
        }

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
    }
}