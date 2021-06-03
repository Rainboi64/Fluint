//
// ClientData.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;
using System.Collections.Generic;
using System.Text;
using Fluint.Layer.Networking.Client;

namespace Fluint.Implementation.Networking.Client
{
    public class ClientData : IClientData
    {
        public ClientData(string username, int gUID)
        {
            Username = username;
            GUID = gUID;
        }

        public string Username { get; }
        public int GUID { get; }
    }
}
