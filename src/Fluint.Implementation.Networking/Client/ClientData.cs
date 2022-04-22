//
// ClientData.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using Fluint.Layer.Networking.Client;

namespace Fluint.Implementation.Networking.Client
{
    public class ClientData : IClientData
    {
        public ClientData(string username, int gUid)
        {
            Username = username;
            Guid = gUid;
        }

        public string Username
        {
            get;
        }

        public int Guid
        {
            get;
        }
    }
}