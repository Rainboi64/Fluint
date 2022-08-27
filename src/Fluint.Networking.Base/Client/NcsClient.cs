// 
// NcsClient.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using System.Net;
using System.Net.Sockets;
using System.Text;
using Fluint.Layer.Diagnostics;
using Fluint.Layer.Networking.Client;
using UdpClient = NetCoreServer.UdpClient;

namespace Fluint.Networking.Base.Client;

public class NcsClient : UdpClient
{
    private readonly ClientData _data;
    private readonly string _ip;
    private readonly ILogger _logger;
    private readonly int _port;

    public NcsClient(string address, int port, ClientData data, ILogger logger) : base(address, port)
    {
        _data = data;
        _ip = address;
        _port = port;
        _logger = logger;
    }


    protected override void OnConnected()
    {
        Send($"hello;{_data.Username};{_data.ID}");
        ReceiveAsync();

        base.OnConnected();
    }


    protected override void OnReceived(EndPoint endpoint, byte[] buffer, long offset, long size)
    {
        var message = Encoding.UTF8.GetString(buffer, (int)offset, (int)size);

        if (message == $"welcome {_data.ID}:{_data.Username}")
        {
            _logger.Information("Fluint connected to [{0}] successfully", endpoint);
            ReceiveAsync();
            return;
        }

        if (message != "Tick!")
        {
            _logger.Information("[{0}] : {1}.", endpoint, message);
        }

        ReceiveAsync();
    }

    protected override void OnError(SocketError error)
    {
        _logger.Information("[{0}] Fluint Client had an error, {1}", Id, error);
        base.OnError(error);
    }
}