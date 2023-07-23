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

    public bool Pick(Ray ray, out ISketch sketch)
    {
        foreach (var item in _sketches)
        {
            var aabb = item.BoundingBox;
            if (!Collision.RayIntersectsBox(ref ray, ref aabb, out float _))
            {
                continue;
            }

            sketch = item;
            return true;
        }

        sketch = null;
        return false;
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