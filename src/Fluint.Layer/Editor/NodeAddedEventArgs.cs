// 
// NodeAddedEventArgs.cs
// 
// Copyright (C) 2023 Yaman Alhalabi
//

using System;

namespace Fluint.Layer.Editor;

public class NodeAddedEventArgs : EventArgs
{
    public INode Node 
    {
        get;
    }

    public NodeAddedEventArgs(INode node)
    {
        Node = node;
    }
}