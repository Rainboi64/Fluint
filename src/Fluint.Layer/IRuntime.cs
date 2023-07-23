//
// IRuntime.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Runtime;

namespace Fluint.Layer;

public interface IRuntime
{
    int Id
    {
        get;
    }

    StartupManifest StartupManifest
    {
        get;
    }

    ModuleManifest ModuleManifest
    {
        get;
    }

    ModulePacket Packet
    {
        get;
    }

    InstanceManager Parent
    {
        get;
    }

    void Create(int id, StartupManifest startupManifest, ModuleManifest moduleManifest, ModulePacket packet,
        InstanceManager parent);

    void Start();
    void Kill();
}