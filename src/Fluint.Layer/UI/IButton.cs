//
// IButton.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;

namespace Fluint.Layer.UI;

[Initialization(InitializationMethod.Scoped)]
public interface IButton : IModule, IGuiComponent
{
    public string Text
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