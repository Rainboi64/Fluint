// 
// ISketch.cs
// 
// Copyright (C) 2022 Yaman Alhalabi

using System.Collections.Generic;
using Fluint.Layer.Graphics.API;

namespace Fluint.Layer.Graphics.Renderers;

[Initialization(InitializationMethod.Scoped)]
public interface ISketch : IModule
{
    IEnumerable<PositionColorVertex> Vertex
    {
        get;
        set;
    }
}