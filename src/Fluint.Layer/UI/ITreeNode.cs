// 
// ITreeNode.cs
// 
// Copyright (C) 2022 Yaman Alhalabi
//
using System.Collections.Generic;

namespace Fluint.Layer.UI;
[Initialization(InitializationMethod.Scoped)]
public interface ITreeNode : IGuiComponent, IDictionary<string, ITreeNode>, IModule 
{
    string Text {
        get;
        set;
    }
}