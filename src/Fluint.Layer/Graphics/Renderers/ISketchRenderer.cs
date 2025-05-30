// 
// ISketchRenderer.cs
// 
// Copyright (C) 2022 Yaman Alhalabi

using Fluint.Layer.Graphics.API;

namespace Fluint.Layer.Graphics.Renderers;

[Initialization(InitializationMethod.Scoped)]
public interface ISketchRenderer : IModule, IRenderer
{
    FillMode FillMode
    {
        get;
        set;
    }
}