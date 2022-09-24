//
// ServerTickedEventArgs.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

namespace Fluint.Layer.Networking.Client.EventArgs
{
    public class ServerTickedEventArgs : System.EventArgs
    {
        public ServerTickedEventArgs(NetworkPacket networkPacket)
        {
            NetworkPacket = networkPacket;
        }

        /// <summary>
        /// The data that the server sent.
        /// </summary>
        public NetworkPacket NetworkPacket
        {
            get;
        }
    }
}