// 
// IWorld.cs
// 
// Copyright (C) 2022 Yaman Alhalabi

namespace Fluint.Layer.EntityComponentSystem;

[Initialization(InitializationMethod.Singleton)]
public interface IWorld : IModule
{
    public T CreateComponent<T>() where T : IComponent;
    public T CreateComponent<T, T2>() where T : IComponent where T2 : IComponent;

    public T GetSystem<T, T2>() where T : ISystem<T2> where T2 : IComponent;
}