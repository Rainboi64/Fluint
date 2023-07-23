// 
// IViewportRenderer.cs
// 
// Copyright (C) 2022 Yaman Alhalabi

using Fluint.Layer.Graphics;
using Fluint.Layer.Graphics.API;
using Fluint.Layer.Graphics.Renderers;
using Fluint.Layer.Windowing;

namespace Fluint.Layer.Editor.Viewport;

[Initialization(InitializationMethod.Scoped)]
public interface IViewportRenderer : IModule
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


    Grid Grid
    {
        get;
    }

    IRenderingPipeline Pipeline
    {
        get;
    }

    ISwapChain SwapChain
    {
        set;
    }

    void Start();
    void Render();
    void Resize(Mathematics.Viewport viewport);
}