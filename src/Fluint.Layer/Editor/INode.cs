// 
// Node.cs
// 
// Copyright (C) 2022 Yaman Alhalabi

using Fluint.Layer.EntityComponentSystem;

namespace Fluint.Layer.Editor;

[Initialization(InitializationMethod.Scoped)]
public interface INode : IComponent, IModule
{
    public string Name
    {
        get;
        set;
    }
}