// 
// ModularAction.cs
// 
// Copyright (C) 2022 Yaman Alhalabi

using System;
using System.Collections;
using System.Collections.Generic;

namespace Fluint.Layer.Functionality;

public class ModularAction : ICollection<Action>
{
    private readonly List<Action> _actions = new();

    public IEnumerator<Action> GetEnumerator()
    {
        return _actions.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)_actions).GetEnumerator();
    }

    public void Add(Action item)
    {
        _actions.Add(item);
    }

    public void Clear()
    {
        _actions.Clear();
    }

    public bool Contains(Action item)
    {
        return _actions.Contains(item);
    }

    public void CopyTo(Action[] array, int arrayIndex)
    {
        _actions.CopyTo(array, arrayIndex);
    }

    public bool Remove(Action item)
    {
        return _actions.Remove(item);
    }

    public int Count => _actions.Count;

    public bool IsReadOnly => false;

    public static implicit operator Action(ModularAction right)
    {
        return right.Invoke;
    }

    public static implicit operator ModularAction(Action right)
    {
        return new ModularAction { right };
    }

    public void Invoke()
    {
        foreach (var action in _actions)
        {
            action.Invoke();
        }
    }

    public void Insert(int key, Action action)
    {
        _actions.Insert(key, action);
    }
}