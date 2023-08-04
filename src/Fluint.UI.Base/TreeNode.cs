// 
// TreeNode.cs
// 
// Copyright (C) 2023 Yaman Alhalabi
//

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Fluint.Layer.UI;
using ImGuiNET;

namespace Fluint.UI.Base;

public class TreeNode : ITreeNode
{
    public ITreeNode this[string key] { get => Nodes[key]; set { Nodes[key] = value; } }

    public IDictionary<string, ITreeNode> Nodes { get; } = new Dictionary<string, ITreeNode>();

    public string Name
    {
        get;
        private set;
    }
    public string Text
    {
        get;
        set;
    }

    public ICollection<string> Keys => Nodes.Keys;

    public ICollection<ITreeNode> Values => Nodes.Values;

    public int Count => Nodes.Count;

    public bool IsReadOnly => Nodes.IsReadOnly;

    public void Add(string key, ITreeNode value)
    {
        Nodes[key] = value;
        value.Begin(key);
    }

    public void Add(KeyValuePair<string, ITreeNode> item)
    {
        Nodes.Add(item);
    }


    public void Begin(string name)
    {
        Name = name;
        foreach(var item in Nodes)
        {
            item.Value.Begin(item.Key);
        }
    }

    public void Clear()
    {
        Nodes.Clear();
    }

    public bool Contains(KeyValuePair<string, ITreeNode> item)
    {
        return Nodes.Contains(item);
    }

    public bool ContainsKey(string key)
    {
        return Nodes.ContainsKey(key);
    }

    public void CopyTo(KeyValuePair<string, ITreeNode>[] array, int arrayIndex)
    {
        Nodes.CopyTo(array, arrayIndex);
    }

    public IEnumerator<KeyValuePair<string, ITreeNode>> GetEnumerator()
    {
        return (IEnumerator<KeyValuePair<string, ITreeNode>>)Nodes;
    }

    public bool Remove(string key)
    {
        return Nodes.Remove(key);
    }

    public bool Remove(KeyValuePair<string, ITreeNode> item)
    {
        return Nodes.Remove(item);
    }

    public void Tick()
    {
        ImGui.TreePush($"{Text}###{Name}");

        ImGui.MenuItem($"{Text}###{Name}_Item");
        var nodes = Nodes.Values;
        foreach(var node in nodes)
        {
            node.Tick();
        }
        ImGui.TreePop();
    }

    public bool TryGetValue(string key, [MaybeNullWhen(false)] out ITreeNode value)
    {
        return Nodes.TryGetValue(key, out value);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return (IEnumerator)Nodes;
    }
}