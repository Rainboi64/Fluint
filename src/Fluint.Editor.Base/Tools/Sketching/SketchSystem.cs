// 
// SketchSystem.cs
// 
// Copyright (C) 2022 Yaman Alhalabi

using System.Collections.Generic;
using Fluint.Layer.Editor.Tools.Sketching;
using Fluint.Layer.EntityComponentSystem;
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

    public IReadOnlyCollection<ISketch> Sketches => _sketches;

    public Vector3[] GetVertices()
    {
        var vertex = new List<Vector3>();
        foreach (var sketch in _sketches)
        {
            vertex.AddRange(sketch.Vertices);
        }

        return vertex.ToArray();
    }
}