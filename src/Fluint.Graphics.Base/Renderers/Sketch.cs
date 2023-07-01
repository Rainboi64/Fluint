// 
// Sketch.cs
// 
// Copyright (C) 2022 Yaman Alhalabi

using System.Collections.Generic;
using Fluint.Layer.Graphics.API;
using Fluint.Layer.Graphics.Renderers;

namespace Fluint.Graphics.Base.Renderers;

public class Sketch : ISketch
{
    public IEnumerable<PositionColorVertex> Vertex
    {
        get;
        set;
    }
}