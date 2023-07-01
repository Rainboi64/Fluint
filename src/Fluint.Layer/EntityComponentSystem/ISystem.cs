// 
// ISystem.cs
// 
// Copyright (C) 2022 Yaman Alhalabi

namespace Fluint.Layer.EntityComponentSystem;

[Initialization(InitializationMethod.Scoped)]
public interface ISystem<in T> : IModule, IComponent
{
    void Register(T component);
}