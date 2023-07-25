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
    private readonly Dictionary<Type, object> _systems = new();

    public World(ModulePacket packet)
    {
        _packet = packet;
    }

    public T CreateComponent<T>() where T : IComponent
    {
        var component = _packet.CreateScoped<T>();
        var system = _systems[typeof(T)] as ISystem<IComponent>;
        system?.Register(component);
        return component;
    }

    public T CreateComponent<T, T2>() where T : IComponent where T2 : IComponent
    {
        var component = _packet.CreateScoped<T>();
        var system = _systems[typeof(T2)] as ISystem<IComponent>;
        system?.Register(component);
        return component;
    }

    public T GetSystem<T, T2>() where T : ISystem<T2> where T2 : IComponent
    {
        if (_systems.ContainsKey(typeof(T2)))
        {
            return (T)_systems[typeof(T2)];
        }

        var system = _packet.CreateScoped<T>();
        _systems[typeof(T2)] = system;
        return system;
    }
}