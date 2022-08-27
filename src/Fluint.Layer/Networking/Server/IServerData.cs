//
// IServerData.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

namespace Fluint.Layer.Networking.Server
{
    /// <summary>
    /// an interface for storing the server's data. 
    /// </summary>
    public struct ServerData
    {
        /// <summary>
        /// The IP Address of the server.
        /// </summary>
        public string IpAddress
        {
            get;
            set;
        }

        /// <summary>
        /// The Port Number for clients to connect through.
        /// </summary>
        public int Port
        {
            get;
            set;
        }

        /// <summary>
        /// The tick delay of the server state, dedicates the tick rate.
        /// </summary>
        /// <value></value>
        public int TickDelay
        {
            get;
            set;
        }

        /// <summary>
        /// The name/tag of the server. (Can be changed at runtime)
        /// </summary>
        public string Name
        {
            get;
            set;
        }
    }
}