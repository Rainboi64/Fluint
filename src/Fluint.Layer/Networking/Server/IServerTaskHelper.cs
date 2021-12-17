//
// IServerTaskHelper.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;
using System.Collections.Generic;
using System.Text;
using Fluint.Layer.Networking.Client;

namespace Fluint.Layer.Networking
{
    [Initialization(InitializationMethod.Scoped)]
    public interface IServerTaskHelper : IModule
    {
        void InvokeConnectedEvent(IClientData client);
        void InvokeDisconnectedEvent(IClientData client, DisconnectionReason reason);
        List<NetworkPacket> PacketsToMultiCast { get; }

    }
}
