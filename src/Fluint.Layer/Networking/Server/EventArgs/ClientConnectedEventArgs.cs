//
// ClientConnectedEventArgs.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;
using Fluint.Layer.Networking.Client;

namespace Fluint.Layer.Networking.Server
{
    /// <summary>
    /// Event arguments for when a client connects.
    /// </summary>
    public class ClientConnectedEventArgs : EventArgs
    {
        public ClientConnectedEventArgs(ClientData client)
        {
            Client = client;
        }

        /// <summary>
        /// The data of the client who connected.
        /// </summary>
        public ClientData Client
        {
            get;
        }
    }
}