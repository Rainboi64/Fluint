// 
// DOMRenderer.cs
// 
// Copyright (C) 2023 Yaman Alhalabi

using System;
using System.Collections.Generic;
using System.Xml;
using Fluint.Layer.UI;

namespace Fluint.UI.Base;

public class XmlNode : IXmlNode
{
    public string Name
    {
        get;
        private set;
    }

    public void Begin(string name)
    {
        Name = name;
        var doc = new XmlDocument();
        doc.LoadXml("<container>" +
                    "<container>" +
                    "Pride And Prejudice" +
                    "</container>" +
                    "</container>");

        for (var i = 0; i < doc.ChildNodes.Count; i++)
        {
            var root = doc.ChildNodes[i];
            ParseXml(root);
        }
    }

    public void Tick()
    {
        foreach (var item in _children.Values)
        {
            item.Tick();
        }
    }

    private void ParseXml(System.Xml.XmlNode node)
    {
        if (node is null)
        {
            return;
        }
        
        switch (node.Name)
        {
            case "container":
                break;
            case "button":
                break;
        }
        
        if (!root.HasChildNodes)
        {
            return;
        }

        for (var i = 0; i < root.ChildNodes.Count; i++)
        {
            var child = root.ChildNodes[i];
            if (child is null)
            {
                continue;
            }
            ParseXml(child);
        }
    }


    private readonly IDictionary<string, IGuiComponent> _children = new Dictionary<string, IGuiComponent>();
    
    public IEnumerator<KeyValuePair<string, IGuiComponent>> GetEnumerator()
    {
        return _children.GetEnumerator();
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