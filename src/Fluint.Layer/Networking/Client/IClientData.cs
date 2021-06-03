//
// IClientData.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

namespace Fluint.Layer.Networking.Client
{
    [Initialization(InitializationMethod.Scoped)]
    public interface IClientData
    {
        /// <summary>
        /// The username of the client.
        /// </summary>
        string Username { get; }

        /// <summary>
        /// The Identifying ID of the client.
        /// </summary>
        int GUID { get; }
    }
}