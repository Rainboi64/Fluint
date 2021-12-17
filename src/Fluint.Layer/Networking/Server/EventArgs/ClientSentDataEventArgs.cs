//
// ClientSentDataEventArgs.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;
using Fluint.Layer.Networking.Client;

namespace Fluint.Layer.Networking.Server
{
    /// <summary>
    /// Event Arguments for when the client sends data.
    /// </summary>
    public class ClientSentDataEventArgs : EventArgs
    {
        
        /// <summary>
        /// The Data The client sent.
        /// </summary>
        public NetworkPacket Data { get; }

        /// <summary>
        /// The data of the client who sent.
        /// </summary>
        public IClientData Client { get; }

        public ClientSentDataEventArgs(NetworkPacket data, IClientData client)
        {
            Data = data;
            Client = client;
        }
    }
}