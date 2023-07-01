// 
// IComponent.cs
// 
// Copyright (C) 2022 Yaman Alhalabi

namespace Fluint.Layer.EntityComponentSystem;

[Initialization(InitializationMethod.Scoped)]
public interface IComponent : IModule
{
    public IEntity Entity
    {
        set;
    }
}