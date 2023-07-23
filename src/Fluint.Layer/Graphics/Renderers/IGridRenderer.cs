// 
// IGridRenderer.cs
// 
// Copyright (C) 2022 Yaman Alhalabi

using Fluint.Layer.Editor.Viewport;

namespace Fluint.Layer.Graphics.Renderers;

[Initialization(InitializationMethod.Scoped)]
public interface IGridRenderer : IModule, IRenderer
{
    public Grid Grid
    {
        get;
        set;
    }
}