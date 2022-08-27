//
// DisconnectedFromServerEventArgs.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;
using Fluint.Layer.Networking.Server;

namespace Fluint.Layer.Networking.Client
{
    public class DisconnectedFromServerEventArgs : EventArgs
    {
        public DisconnectedFromServerEventArgs(DisconnectionReason reason, ServerData serverInfo)
        {
            Reason = reason;
            ServerInfo = serverInfo;
        }

        /// <summary>
        /// The reason the client disconnected.
        /// </summary>
        public DisconnectionReason Reason
        {
            get;
        }

        /// <summary>
        /// The server disconnected from.
        /// </summary>
        public ServerData ServerInfo
        {
            get;
        }
    }
}