// 
// NodeSystem.cs
// 
// Copyright (C) 2022 Yaman Alhalabi
//

using System;
using System.Collections.Generic;
using Fluint.Layer.Editor;

namespace Fluint.Editor.Base;

public class NodeSystem : INodeSystem
{
    private List<INode> _nodes = new();
   
    public IReadOnlyCollection<INode> Nodes
    {
        get => _nodes;
    }
   
    public event EventHandler<NodeAddedEventArgs> Add;

    public void Register(INode component)
    {
        _nodes.Add(component);
        Add?.Invoke(this, new NodeAddedEventArgs(component));
    }
}
