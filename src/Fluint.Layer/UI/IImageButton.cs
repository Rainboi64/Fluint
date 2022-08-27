//
// IButton.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;
using Fluint.Layer.Mathematics;

namespace Fluint.Layer.UI;

[Initialization(InitializationMethod.Scoped)]
public interface IImageButton : IModule, IGuiComponent
{
    public string Text
    {
        get;
        set;
    }

    public string Path
    {
        get;
        set;
    }

    public Color4 BackgroundColor
    {
        get;
        set;
    }

    public float Padding
    {
        get;
        set;
    }

    public Vector2i Size
    {
        get;
        set;
    }

    public Action OnClick
    {
        get;
        set;
    }
}