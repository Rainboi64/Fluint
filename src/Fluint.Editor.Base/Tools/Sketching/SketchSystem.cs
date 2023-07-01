// 
// SketchSystem.cs
// 
// Copyright (C) 2022 Yaman Alhalabi

using System.Collections.Generic;
using Fluint.Layer.Editor.Tools.Sketching;
using Fluint.Layer.EntityComponentSystem;
using Fluint.Layer.Graphics.API;
using Fluint.Layer.Mathematics;

namespace Fluint.Editor.Base.Tools.Sketching;

public class SketchSystem : ISketchSystem
{
    private readonly List<ISketch> _sketches = new();

    public IEntity Entity
    {
        get;
        set;
    }

    public void Register(ISketch component)
    {
        _sketches.Add(component);
    }

    public ISketch Pick(Ray ray)
    {
        foreach (var sketch in _sketches)
        {
            if (!sketch.BoundingBox.Intersects(ref ray))
            {
                continue;
            }

            foreach (var vertex in sketch.Vertex)
            {
                var boundingSphere = new BoundingSphere(vertex.Position, 1);
                if (Collision.RayIntersectsSphere(ref ray, ref boundingSphere,
                        out float distance))
                {
                    return sketch;
                }
            }
        }

        return null;
    }

    public PositionColorVertex[] GetVertex()
    {
        var vertex = new List<PositionColorVertex>();
        foreach (var sketch in _sketches)
        {
            vertex.AddRange(sketch.Vertex);
        }

        return vertex.ToArray();
    }
}