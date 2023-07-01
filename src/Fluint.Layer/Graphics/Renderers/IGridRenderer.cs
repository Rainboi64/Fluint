// 
// IGridRenderer.cs
// 
// Copyright (C) 2022 Yaman Alhalabi

using Fluint.Layer.Editor;
using Fluint.Layer.Graphics.Common;
using Fluint.Layer.Mathematics;

namespace Fluint.Layer.Graphics.Renderers;

[Initialization(InitializationMethod.Scoped)]
public interface IGridRenderer : IModule, IRenderer
{
    public Grid Grid
    {
        get;
        set;
    }

    public ModelViewProjection WorldView
    {
        get;
        set;
    }

    public Viewport Viewport
    {
        get;
        set;
    }
}