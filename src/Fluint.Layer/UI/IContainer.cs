//
// IContainer.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using Fluint.Layer.Mathematics;

namespace Fluint.Layer.UI;

[Initialization(InitializationMethod.Scoped)]
public interface IContainer : IModule, IGuiContainer<IGuiComponent>
{
    string Title
    {
        get;
        set;
    }

    bool IsFocused
    {
        get;
    }

    bool Resizable
    {
        get;
        set;
    }

    bool ScrollBar
    {
        get;
        set;
    }

    Vector2i Size
    {
        get;
    }

    Vector2i Location
    {
        get;
    }
}