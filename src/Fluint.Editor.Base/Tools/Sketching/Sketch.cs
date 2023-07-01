// 
// Sketch.cs
// 
// Copyright (C) 2022 Yaman Alhalabi

using System;
using Fluint.Layer.Editor.Tools.Sketching;
using Fluint.Layer.EntityComponentSystem;
using Fluint.Layer.Graphics.API;
using Fluint.Layer.Mathematics;

namespace Fluint.Editor.Base.Tools.Sketching;

public class Sketch : ISketch
{
    public Sketch()
    {
        Vertex = Array.Empty<PositionColorVertex>();
    }

    public IEntity Entity
    {
        get;
        set;
    }

    public BoundingBox BoundingBox
    {
        get;
        private set;
    }

    public void Update()
    {
        var min = Vector3.Zero;
        var max = Vector3.Zero;

        foreach (var item in Vertex)
        {
            min = Vector3.Min(item.Position, min);
            max = Vector3.Max(item.Position, min);
        }

        BoundingBox = new BoundingBox(min, max);
    }

    public PositionColorVertex[] Vertex
    {
        get;
        set;
    }
}