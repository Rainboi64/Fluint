//
// IClient.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using Fluint.Layer.Diagnostics;
using Fluint.Layer.Networking.Client;
using Fluint.Layer.Networking.Server;

namespace Fluint.Networking.Base.Client
{
    internal class Client : IClient
    {
        private readonly ILogger _logger;
        private NcsClient _client;

        public Client(ILogger logger)
        {
            _logger = logger;
        }

        public ClientData ClientInfo
        {
            get;
            set;
        }

        public ServerData Server
        {
            get;
        }

        public bool IsConnected => _client?.IsConnected ?? false;

        public void Connect(ServerData serverInfo)
        {
            _client = new NcsClient(serverInfo.IpAddress, serverInfo.Port, ClientInfo, _logger);
            _client.Connect();
        }

        public void Disconnect()
        {
            _client.Send("bye");
            _client.Disconnect();
        }

        public void SendMessage(string data)
        {
            _client.Send(data);
        }
    }
}