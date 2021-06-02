using System;
using Fluint.Layer.Networking.Client;

namespace Fluint.Layer.Networking.Server
{
    /// <summary>
    /// Event Arguments for when the clients disconnects.
    /// </summary>
    public class ClientDisconnectedEventArgs : EventArgs
    {

        /// <summary>
        /// The reason the client disconnected.
        /// </summary>
        public DisconnectionReason Reason { get; }

        /// <summary>
        /// The data of the client who disconnected.
        /// </summary>
        public IClientData Client { get; }
        
        public ClientDisconnectedEventArgs(IClientData client, DisconnectionReason reason)
        {
            Client = client;
            Reason = reason;
        }
    }
}