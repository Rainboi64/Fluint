//
// IClient.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using Fluint.Layer.Networking.Server;

namespace Fluint.Layer.Networking.Client
{
    /// <summary>
    /// The managing class on the Client-Side.
    /// </summary>
    [Initialization(InitializationMethod.Singleton)]
    public interface IClient : IModule
    {
        /// <summary>
        /// The info of this Client
        /// </summary>
        ClientData ClientInfo
        {
            get;
            set;
        }

        /// <summary>
        /// Server Connected To.
        /// </summary>
        ServerData Server
        {
            get;
        }

        /// <summary>
        /// The connection state of this client.
        /// </summary>
        bool IsConnected
        {
            get;
        }

        /// <summary>
        /// Connects to server. (Sets <see cref="Server"/>)
        /// </summary>
        /// <param name="serverInfo">Server to connect to.</param>
        void Connect(ServerData serverInfo);

        /// <summary>
        /// Disconnects from the server.
        /// </summary>
        void Disconnect();

        /// <summary>
        /// Sends a message to all clients.
        /// </summary>
        void SendMessage(string data);
    }
}