// 
// IUILayer.cs
// 
// Copyright (C) 2023 Yaman Alhalabi

using System.Collections.Generic;

namespace Fluint.Layer.UI;

[Initialization(InitializationMethod.Scoped)]
public interface IUILayer : IModule
{
    T SpawnControl<T>() where T : Control;
    public IDictionary<string, IGuiComponent> Controls
    {
        get;
    }

    public bool Dockspace
    {
        get;
        set;
    }

    void Render();
    void Load();
    void InitializeContext(UIContext context);
}