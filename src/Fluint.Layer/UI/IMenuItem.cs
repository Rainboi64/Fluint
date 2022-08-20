// 
// MenuItem.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using System;
using Fluint.Layer.Functionality;

namespace Fluint.Layer.UI;

[Initialization(InitializationMethod.Scoped)]
public interface IMenuItem : IModule, IGuiContainer<IMenuItem>
{
    string Text
    {
        get;
        set;
    }

    ModularAction OnClick { get; set; }
}