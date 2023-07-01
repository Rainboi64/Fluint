// 
// Modal.cs
// 
// Copyright (C) 2022 Yaman Alhalabi

using System;
using System.Collections;
using System.Collections.Generic;
using Fluint.Layer.UI;
using ImGuiNET;

namespace Fluint.UI.Base;

public class Modal : IModal
{
    private readonly Dictionary<string, IGuiComponent> _components = new();
    private bool _open;

    public string Name
    {
        get;
        private set;
    }

    public void Begin(string name)
    {
        Name = name;
        foreach (var item in _components)
        {
            item.Value.Begin(item.Key);
        }
    }

    public void Tick()
    {
        if (_open)
        {
            ImGui.OpenPopup($"{Title}###{Name}");
        }

        if (ImGui.BeginPopupModal($"{Title}###{Name}", ref _open))
        {
            return;
        }

        foreach (var item in _components)
        {
            item.Value.Tick();
        }
    }

    public string Title
    {
        get;
        set;
    }

    public bool Open
    {
        get => _open;
        set => _open = value;
    }

    public IEnumerator<KeyValuePair<string, IGuiComponent>> GetEnumerator()
    {
        return _components.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)_components).GetEnumerator();
    }

    public void Add(KeyValuePair<string, IGuiComponent> item)
    {
        _components.Add(item.Key, item.Value);
    }

    public void Clear()
    {
        _components.Clear();
    }

    public bool Contains(KeyValuePair<string, IGuiComponent> item)
    {
        return _components.ContainsKey(item.Key);
    }

    public void CopyTo(KeyValuePair<string, IGuiComponent>[] array, int arrayIndex)
    {
        throw new NotImplementedException();
    }

    public bool Remove(KeyValuePair<string, IGuiComponent> item)
    {
        return _components.Remove(item.Key);
    }

    public int Count => _components.Count;

    public bool IsReadOnly => false;

    public void Add(string key, IGuiComponent value)
    {
        _components.Add(key, value);
    }

    public bool ContainsKey(string key)
    {
        return _components.ContainsKey(key);
    }

    public bool Remove(string key)
    {
        return _components.Remove(key);
    }

    public bool TryGetValue(string key, out IGuiComponent value)
    {
        return _components.TryGetValue(key, out value);
    }

    public IGuiComponent this[string key]
    {
        get => _components[key];
        set => _components[key] = value;
    }

    public ICollection<string> Keys => _components.Keys;

    public ICollection<IGuiComponent> Values => _components.Values;
}