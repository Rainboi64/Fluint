﻿//
// Container.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System.Collections;
using System.Collections.Generic;
using Fluint.Layer.Mathematics;
using Fluint.Layer.UI;
using ImGuiNET;

namespace Fluint.UI.Base;

public class Container : IContainer
{
    private readonly IDictionary<string, IGuiComponent> _children = new Dictionary<string, IGuiComponent>();

    public string Title
    {
        get;
        set;
    }

    public Vector2i Size
    {
        get;
        private set;
    }

    public Vector2i Location
    {
        get;
        private set;
    }

    public string Name
    {
        get;
        private set;
    }

    public bool ScrollBar
    {
        get;
        set;
    } = true;

    public bool IsFocused
    {
        get;
        private set;
    }

    public bool Resizable
    {
        get;
        set;
    } = true;

    public void Begin(string name)
    {
        Name = name;
    }

    public void Tick()
    {
        var flags = ImGuiWindowFlags.None;

        if (!ScrollBar)
        {
            flags |= ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse |
                     ImGuiWindowFlags.NoTitleBar;
        }

        if (!Resizable)
        {
            flags |= ImGuiWindowFlags.NoResize;
        }

        ImGui.Begin($"{Title}###{Name}", flags);

        var size = ImGui.GetWindowContentRegionMax();
        Size = new Vector2i((int)size.X, (int)size.Y);


        var location = ImGui.GetWindowPos() + ImGui.GetWindowContentRegionMin();
        Location = new Vector2i((int)location.X, (int)location.Y);

        IsFocused = ImGui.IsWindowFocused();

        foreach (var item in _children.Values)
        {
            item.Tick();
        }

        ImGui.End();
    }

    public IEnumerator<KeyValuePair<string, IGuiComponent>> GetEnumerator()
    {
        return _children.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Add(KeyValuePair<string, IGuiComponent> item)
    {
        _children.Add(item);
    }

    public void Clear()
    {
        _children.Clear();
    }

    public bool Contains(KeyValuePair<string, IGuiComponent> item)
    {
        return _children.Contains(item);
    }

    public void CopyTo(KeyValuePair<string, IGuiComponent>[] array, int arrayIndex)
    {
        _children.CopyTo(array, arrayIndex);
    }

    public bool Remove(KeyValuePair<string, IGuiComponent> item)
    {
        return _children.Remove(item);
    }

    public int Count => _children.Count;

    public bool IsReadOnly => false;

    public void Add(string key, IGuiComponent value)
    {
        _children.Add(key, value);
    }

    public bool ContainsKey(string key)
    {
        return _children.ContainsKey(key);
    }

    public bool Remove(string key)
    {
        return _children.Remove(key);
    }

    public bool TryGetValue(string key, out IGuiComponent value)
    {
        return _children.TryGetValue(key, out value);
    }

    public IGuiComponent this[string key]
    {
        get => _children[key];
        set => _children[key] = value;
    }

    public ICollection<string> Keys => _children.Keys;

    public ICollection<IGuiComponent> Values => _children.Values;
}