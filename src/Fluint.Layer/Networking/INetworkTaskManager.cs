//
// INetworkTaskManager.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

namespace Fluint.Layer.Networking;

[Initialization(InitializationMethod.Scoped)]
public interface INetworkTaskManager
{
    void Parse(string task);
}