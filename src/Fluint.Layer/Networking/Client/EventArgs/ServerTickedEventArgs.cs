//
// ServerTickedEventArgs.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;

namespace Fluint.Layer.Networking.Client
{
    public class ServerTickedEventArgs : EventArgs
    {
        public ServerTickedEventArgs(NetworkPacket networkPacket)
        {
            NetworkPacket = networkPacket;
        }

        /// <summary>
        /// The data that the server sent.
        /// </summary>
        public NetworkPacket NetworkPacket { get; }
    }
}