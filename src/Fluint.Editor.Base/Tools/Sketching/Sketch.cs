// 
// Sketch.cs
// 
// Copyright (C) 2022 Yaman Alhalabi

using System;
using System.Collections.Generic;
using Fluint.Layer.Editor.Tools.Sketching;
using Fluint.Layer.EntityComponentSystem;
using Fluint.Layer.Graphics.API;
using Fluint.Layer.Graphics.Debug;
using Fluint.Layer.Mathematics;

namespace Fluint.Editor.Base.Tools.Sketching;

public class Sketch : ISketch
{
    public Sketch(IDebugServer debug)
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
            max = Vector3.Max(item.Position, max);
        }

        BoundingBox = new BoundingBox(min, max);
    }

    public int PickVertex(Ray ray, float radius)
    {
        for (var i = 0; i < Vertex.Length; i++)
        {
            var boundingBox = new BoundingBox(Vertex[i].Position - Vector3.One * radius,
                Vertex[i].Position + Vector3.One * radius);
            if (Collision.RayIntersectsBox(ref ray, ref boundingBox,
                    out float distance))
            {
                return i;
            }
        }

        return -1;
    }

    public int[] PickMultipleVertex(Ray ray, float radius)
    {
        var temp = new List<int>();
        for (var i = 0; i < Vertex.Length; i++)
        {
            var boundingBox = new BoundingBox(Vertex[i].Position - Vector3.One * radius,
                Vertex[i].Position + Vector3.One * radius);
            if (Collision.RayIntersectsBox(ref ray, ref boundingBox,
                    out float distance))
            {
                temp.Add(i);
            }
        }

        return temp.ToArray();
    }

    public PositionColorVertex[] Vertex
    {
        get;
        set;
    }
}