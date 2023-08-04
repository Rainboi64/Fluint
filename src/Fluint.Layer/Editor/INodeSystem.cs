// 
// INodeSystem.cs
// 
// Copyright (C) 2023 Yaman Alhalabi
//

using System;
using System.Collections.Generic;
using Fluint.Layer.EntityComponentSystem;

namespace Fluint.Layer.Editor;

[Initialization(InitializationMethod.Scoped)]
public interface INodeSystem : ISystem<INode>
{
    IReadOnlyCollection<INode> Nodes
    {
        get;
    }
    event EventHandler<NodeAddedEventArgs> Add;
}