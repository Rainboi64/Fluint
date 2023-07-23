// 
// MenuItem.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using System.Collections;
using System.Collections.Generic;
using Fluint.Layer.Functionality;
using Fluint.Layer.UI;
using ImGuiNET;

namespace Fluint.UI.Base;

public class MenuItem : IMenuItem
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
        if (_children.Count > 0)
        {
            if (!ImGui.BeginMenu($"{Text}###{Name}"))
            {
                return;
            }

            OnClick?.Invoke();
            foreach (var item in _children.Values)
            {
                item.Tick();
            }

            ImGui.EndMenu();
        }
        else
        {
            if (ImGui.MenuItem($"{Text}###{Name}"))
            {
                OnClick?.Invoke();
            }
        }
    }

    public string Text
    {
        get;
        set;
    }

    public ModularAction OnClick
    {
        get;
        set;
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
        return _children.Remove(item);
    }

    public int Count => _children.Count;

    public bool IsReadOnly => false;
}