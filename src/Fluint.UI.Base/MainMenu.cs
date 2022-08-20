// 
// MainMenu.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using System.Collections;
using System.Collections.Generic;
using Fluint.Layer.UI;
using ImGuiNET;

namespace Fluint.UI.Base;

public class MainMenu : IMainMenu
{
    private readonly IDictionary<string, IMenuItem> _children = new Dictionary<string, IMenuItem>();

    public string Name
    {
        get;
        private set;
    }

    public void Begin(string name)
    {
        Name = name;
    }

    public void Tick()
    {
        ImGui.BeginMainMenuBar();
        foreach (var item in _children.Values)
        {
            item.Tick();
        }
        ImGui.EndMainMenuBar();
    }

    public void Add(string name, IMenuItem item)
    {
        item.Begin(name);
        _children.Add(name, item);
    }

    public bool ContainsKey(string key)
    {
        return _children.ContainsKey(key);
    }

    public bool Remove(string key)
    {
        return _children.Remove(key);
    }

    public bool TryGetValue(string key, out IMenuItem value)
    {
        return _children.TryGetValue(key, out value);
    }

    public IMenuItem this[string key]
    {
        get => _children[key];
        set
        {
            if (!_children.ContainsKey(key))
            {
                Add(key, value);
                return;
            }
            _children[key] = value;
        }
    }

    public ICollection<string> Keys => _children.Keys;

    public ICollection<IMenuItem> Values => _children.Values;

    public IEnumerator<KeyValuePair<string, IMenuItem>> GetEnumerator()
    {
        return _children.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _children.GetEnumerator();
    }

    public void Add(KeyValuePair<string, IMenuItem> item)
    { 
        item.Value.Begin(item.Key);
        _children.Add(item);
    }

    public void Clear()
    {
        _children.Clear();
    }

    public bool Contains(KeyValuePair<string, IMenuItem> item)
    {
        return _children.Contains(item);
    }

    public void CopyTo(KeyValuePair<string, IMenuItem>[] array, int arrayIndex)
    {
        _children.CopyTo(array, arrayIndex);
    }

    public bool Remove(KeyValuePair<string, IMenuItem> item)
    {
        return _children.Remove(item.Key);
    }

    public int Count
    {
        get => _children.Count;
    }

    public bool IsReadOnly => false;
}