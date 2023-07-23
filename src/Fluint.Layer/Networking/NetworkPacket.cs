//
// NetworkPacket.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

namespace Fluint.Layer.Networking;

/// <summary>
///     A data structure to be transported
/// </summary>
public class NetworkPacket
{
    private const string ConnectTask = "CLIENT_CONNECT_REQUEST";
    private const string DisconnectTask = "CLIENT_DISCONNECT_REQUEST";
    private const string ChatTask = "CLIENT_SENT_MESSAGE";
    private const string TickTask = "SERVER_TICK";

    public NetworkPacket(string task, object[] arguments)
    {
        Task = task;
        Arguments = arguments;
    }

    public string Task
    {
        get;
    }

    public object[] Arguments
    {
        get;
    }
}