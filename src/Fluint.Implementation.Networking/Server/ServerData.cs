//
// ServerData.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;
using System.Collections.Generic;
using System.Text;
using Fluint.Layer.Networking.Server;

namespace Fluint.Implementation.Networking.Server
{
    public class ServerData : IServerData
    {
        public ServerData(string ipAddress, int port, int tickDelay, string name)
        {
            IpAddress = ipAddress;
            Port = port;
            TickDelay = tickDelay;
            Name = name;
        }

        public string IpAddress { get; }

        public int Port { get; }

        public int TickDelay { get; }

        public string Name { get; set; }
    }
}
