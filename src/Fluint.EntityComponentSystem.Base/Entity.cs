// 
// Entity.cs
// 
// Copyright (C) 2022 Yaman Alhalabi

using Fluint.Layer.EntityComponentSystem;

namespace Fluint.EntityComponentSystem.Base;

public class Entity : IEntity
{
    private readonly List<IComponent> _components = new();

    public int Id
    {
        get;
        set;
    }

    public ICollection<IComponent> Components => _components;

    public bool GetComponent<T>(out T component) where T : IComponent
    {
        foreach (var item in _components)
        {
            if (item.GetType() == typeof(T))
            {
                component = (T)item;
                return true;
            }
        }

        component = (T)_components[0];
        return false;
    }

    public void AddComponent(IComponent component)
    {
        component.Entity = this;
        _components.Add(component);
    }
}