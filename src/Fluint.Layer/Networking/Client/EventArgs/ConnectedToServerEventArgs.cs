//
// ConnectedToServerEventArgs.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System.Collections.Generic;
using System.Collections.ObjectModel;
using Fluint.Layer.Networking.Server;

namespace Fluint.Layer.Networking.Client.EventArgs;

/// <summary>
///     event arguments for when the client is connected to the server.
/// </summary>
public class ConnectedToServerEventArgs : System.EventArgs
{
    public ConnectedToServerEventArgs(ServerData serverInfo, IEnumerable<ClientData> clientsConnected)
    {
        ServerInfo = serverInfo;
        ClientsConnected = (ReadOnlyCollection<ClientData>)clientsConnected;
    }

    /// <summary>
    ///     The info of the server connected to.
    /// </summary>
    public ServerData ServerInfo
    {
        get;
    }

    /// <summary>
    ///     The Collection of clients who are connected to this server, including this one.
    /// </summary>
    public ReadOnlyCollection<ClientData> ClientsConnected
    {
        get;
    }
}