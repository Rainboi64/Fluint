// 
// IEntity.cs
// 
// Copyright (C) 2022 Yaman Alhalabi

using System.Collections.Generic;

namespace Fluint.Layer.EntityComponentSystem;

public interface IEntity
{
    int Id
    {
        get;
        set;
    }

    ICollection<IComponent> Components
    {
        get;
    }

    bool GetComponent<T>(out T component) where T : IComponent;
    void AddComponent(IComponent component);
}