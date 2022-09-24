//
// ClientSentDataEventArgs.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using Fluint.Layer.Networking.Client;

namespace Fluint.Layer.Networking.Server.EventArgs
{
    /// <summary>
    /// Event Arguments for when the client sends data.
    /// </summary>
    public class ClientSentDataEventArgs : System.EventArgs
    {
        public ClientSentDataEventArgs(NetworkPacket data, ClientData client)
        {
            Data = data;
            Client = client;
        }

        /// <summary>
        /// The Data The client sent.
        /// </summary>
        public NetworkPacket Data
        {
            get;
        }

        /// <summary>
        /// The data of the client who sent.
        /// </summary>
        public ClientData Client
        {
            get;
        }
    }
}