using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using Fluint.Layer.Networking;
using NetCoreServer;
using Newtonsoft.Json;

namespace Fluint.Implementation.Networking.Server
{
    internal class ServerTcpSession : TcpSession
    {
        public List<INetworkPacket> PacketsReceived = new List<INetworkPacket>();
        public ServerTcpSession(TcpServer server) : base(server) { }

        protected override void OnReceived(byte[] buffer, long offset, long size)
        {
            var message = Encoding.UTF8.GetString(buffer, (int)offset, (int)size);
            PacketsReceived.Add(JsonConvert.DeserializeObject<INetworkPacket>(message));
        }

        protected override void OnError(SocketError error)
        {
            Console.WriteLine($"Chat TCP session caught an error with code {error}");
        }
    }

}
