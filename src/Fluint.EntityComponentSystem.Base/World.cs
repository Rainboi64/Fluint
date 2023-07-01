// 
// World.cs
// 
// Copyright (C) 2022 Yaman Alhalabi

using Fluint.Layer.DependencyInjection;
using Fluint.Layer.EntityComponentSystem;

namespace Fluint.EntityComponentSystem.Base;

public class World : IWorld
{
    private readonly ModulePacket _packet;
    private readonly Dictionary<Type, ISystem<IComponent>> _systems = new();

    public World(ModulePacket packet)
    {
        _packet = packet;
    }

    public T CreateComponent<T>() where T : IComponent
    {
        var component = (ISystem<IComponent>)_packet.CreateScoped<T>();
        _systems[typeof(T)].Register(component);
        return (T)component;
    }

    public T CreateSystem<T, T2>() where T : ISystem<T2> where T2 : IComponent
    {
        var system = (ISystem<IComponent>)_packet.CreateScoped<T>();
        _systems[typeof(T2)] = system;
        return (T)system;
    }
}