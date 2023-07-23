// 
// Node..cs
// 
// Copyright (C) 2022 Yaman Alhalabi

using System.Collections.Generic;
using System.Linq;
using Fluint.Layer.EntityComponentSystem;

namespace Fluint.Layer.Editor;

public class Node : IEntity
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
        foreach (var item in _components.OfType<T>())
        {
            component = item;
            return true;
        }

        component = default;
        return false;
    }

    public void AddComponent(IComponent component)
    {
        _components.Add(component);
    }
}