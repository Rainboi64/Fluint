// 
// IViewportContext.cs
// 
// Copyright (C) 2022 Yaman Alhalabi
//

using System;
using Fluint.Layer.Graphics;
using Fluint.Layer.Input;
using Fluint.Layer.StateManagement;
using Fluint.Layer.Windowing;

namespace Fluint.Layer.Editor.Viewport;

[Initialization(InitializationMethod.Scoped)]
public interface IViewportContext : IModule, IStatefulContext
{
    IWindow Window
    {
        get;
        set;
    }

    ICamera Camera
    {
        get;
    }

    IInputManager InputManager
    {
        get;
    }

    IBindingsManager BindingsManager
    {
        get;
    }

    Mathematics.Viewport Viewport
    {
        get;
    }

    bool Focused
    {
        get;
        set;
    }

    void Start();
    void Update();
    void Resize(Mathematics.Viewport viewport);
    void AttachRenderer(IViewportRenderer renderer);

    // Add more useful events later...
    event EventHandler<TickEventArgs> OnTick;
}