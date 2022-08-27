//
// IServerTaskHelper.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System.Collections.Generic;
using Fluint.Layer.Networking.Client;

namespace Fluint.Layer.Networking
{
    [Initialization(InitializationMethod.Scoped)]
    public interface IServerTaskHelper : IModule
    {
        List<NetworkPacket> PacketsToMultiCast
        {
            get;
        }

        void InvokeConnectedEvent(ClientData client);
        void InvokeDisconnectedEvent(ClientData client, DisconnectionReason reason);
    }
}