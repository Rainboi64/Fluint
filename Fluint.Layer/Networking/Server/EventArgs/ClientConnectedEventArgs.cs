using System;
using Fluint.Layer.Networking.Client;

namespace Fluint.Layer.Networking.Server
{
    /// <summary>
    /// Event arguments for when a client connects.
    /// </summary>
    public class ClientConnectedEventArgs : EventArgs
    {
        /// <summary>
        /// The data of the client who connected.
        /// </summary>
        public IClientData Client { get; }

        public ClientConnectedEventArgs(IClientData client) 
        { 
            Client = client;
        }
    }
}