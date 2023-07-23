//
// IServerTask.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

namespace Fluint.Layer.Networking.Server;

[Initialization(InitializationMethod.Instanced)]
public interface IServerTask : IModule
{
    string TaskId
    {
        get;
    }

    void Do(ref IServerTaskHelper helper);
}