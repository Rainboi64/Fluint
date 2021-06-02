using System;
using System.Collections.Generic;
using Fluint.Layer.Networking.Server;

namespace Fluint.Layer.Networking.Client
{
    /// <summary>
    /// The managing class on the Client-Side.
    /// </summary>
    public interface IClient : IModule
    {
        /// <summary>
        /// The info of this Client
        /// </summary>
        IClientData ClientInfo { get; }

        /// <summary>
        /// Server Connected To.
        /// </summary>
        IServerData Server { get; }

        /// <summary>
        /// The connection state of this client.
        /// </summary>
        bool IsConnected { get; }

        /// <summary>
        /// Connects to server. (Sets <see cref="Server"/>)
        /// </summary>
        /// <param name="serverInfo">Server to connect to.</param>
        void Connect(IServerData serverInfo);

        /// <summary>
        /// Disconnects from server (should call <see cref="DisconnectedFromServer"/>)
        /// </summary>
        void Disconnect();

        /// <summary>
        /// Sends a message to all clients.
        /// </summary>
        void SendMessage();

        /// <summary>
        /// Called when this client connects.
        /// </summary>
        event EventHandler<ConnectedToServerEventArgs> ConnectedToServer;

        /// <summary>
        /// Called when this client disconnects.
        /// </summary>
        event EventHandler<DisconnectedFromServerEventArgs> DisconnectedFromServer;

        /// <summary>
        /// Called when a message is received from another client. (will get called probably after event tick if a message is received)
        /// </summary>
        event EventHandler<MessageReceivedEventArgs> MessageReceived;

        /// <summary>
        /// Called the server ticks (Updates).
        /// </summary>
        event EventHandler<ServerTickedEventArgs> ServerTicked;
    }
}