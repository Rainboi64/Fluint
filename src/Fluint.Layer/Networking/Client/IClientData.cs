//
// IClientData.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;

namespace Fluint.Layer.Networking.Client;

public struct ClientData
{
    /// <summary>
    ///     The username of the cslient.
    /// </summary>
    public string Username
    {
        get;
        set;
    }

    /// <summary>
    ///     The Identifying ID of the client.
    /// </summary>
    public Guid ID
    {
        get;
        set;
    }
}