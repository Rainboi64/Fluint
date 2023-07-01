// 
// ISystem.cs
// 
// Copyright (C) 2022 Yaman Alhalabi

namespace Fluint.Layer.EntityComponentSystem;

public interface ISystem<in T> : IModule where T : IComponent
{
    void Register(T component);
}