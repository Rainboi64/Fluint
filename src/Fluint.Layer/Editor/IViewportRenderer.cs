// 
// IViewportRenderer.cs
// 
// Copyright (C) 2022 Yaman Alhalabi

using Fluint.Layer.Graphics;
using Fluint.Layer.Graphics.API;
using Fluint.Layer.Graphics.Renderers;
using Fluint.Layer.Mathematics;

namespace Fluint.Layer.Editor;

[Initialization(InitializationMethod.Scoped)]
public interface IViewportRenderer : IModule
{
    ICamera Camera
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
    void Resize(Viewport viewport);
}