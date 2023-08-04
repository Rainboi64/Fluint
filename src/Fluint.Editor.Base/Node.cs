// 
// Node.cs
// 
// Copyright (C) 2022 Yaman Alhalabi
//

using System;
using System.Collections.Generic;
using Fluint.Layer.Editor;
using Fluint.Layer.EntityComponentSystem;

namespace Fluint.Editor.Base;

public class Node : INode
{
    public string Name
    {
        get;
        set;
    }
    public IEntity Entity
    {
        get;
        set;
    }
}