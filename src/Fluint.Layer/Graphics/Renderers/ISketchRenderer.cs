// 
// ISketchRenderer.cs
// 
// Copyright (C) 2022 Yaman Alhalabi

using System.Collections.Generic;

namespace Fluint.Layer.Graphics.Renderers;

[Initialization(InitializationMethod.Scoped)]
public interface ISketchRenderer : IModule, IRenderer, ICollection<ISketch>
{
    ISketch this[int index]
    {
        get;
        set;
    }
}