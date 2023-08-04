// 
// ITreeView.cs
// 
// Copyright (C) 2022 Yaman Alhalabi
//
using System.Collections.Generic;
using Fluint.Layer.Mathematics;

namespace Fluint.Layer.UI;
[Initialization(InitializationMethod.Scoped)]
public interface ITreeView : IGuiComponent, IDictionary<string, ITreeNode>, IModule 
{
    string Text {
        get;
        set;
    }

    Vector2i Size {
        get; set;
    }
}