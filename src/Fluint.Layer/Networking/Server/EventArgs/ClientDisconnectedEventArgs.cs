//
// ClientDisconnectedEventArgs.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using Fluint.Layer.Networking.Client;

namespace Fluint.Layer.Networking.Server.EventArgs
{
    /// <summary>
    /// Event Arguments for when the clients disconnects.
    /// </summary>
    public class ClientDisconnectedEventArgs : System.EventArgs
    {
        public ClientDisconnectedEventArgs(ClientData client, DisconnectionReason reason)
        {
            Client = client;
            Reason = reason;
        }

        /// <summary>
        /// The reason the client disconnected.
        /// </summary>
        public DisconnectionReason Reason
        {
            get;
        }

        /// <summary>
        /// The data of the client who disconnected.
        /// </summary>
        public ClientData Client
        {
            get;
        }
    }
}