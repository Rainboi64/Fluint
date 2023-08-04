// 
// IEntity.cs
// 
// Copyright (C) 2023 Yaman Alhalabi
//

using System;
using System.Collections.Generic;

namespace Fluint.Layer.EntityComponentSystem;

public class Entity : IEntity
{
    private int _id;
    private Dictionary<Type, IComponent> _components = new Dictionary<Type, IComponent>();

    public int Id
    {
        get => _id;
        set => _id = value;
    }

    public ICollection<IComponent> Components => _components.Values;

    public void AddComponent(IComponent component)
    {
        _components.Add(component.GetType(), component);
    }

    public bool GetComponent<T>(out T component) where T : IComponent
    {
        if (_components.ContainsKey(typeof(T)))
        {
            component = (T)_components[typeof(T)];
            return true;
        }

        component = default(T);
        return false;
    }
}
