// 
// SketchSystem.cs
// 
// Copyright (C) 2022 Yaman Alhalabi

using System;
using System.Collections.Generic;
using Fluint.Layer.Editor.Tools.Sketching;
using Fluint.Layer.EntityComponentSystem;
using Fluint.Layer.Mathematics;

namespace Fluint.Editor.Base.Tools.Sketching;

public class SketchSystem : ISketchSystem
{
    private readonly List<ISketch> _sketches = new();
    private readonly List<Color> _colors = new();
    private readonly Random _random = new();

    public IEntity Entity
    {
        get;
        set;
    }

    public void Register(ISketch component)
    {
        _sketches.Add(component);
        _colors.Add(_random.NextColor());
    }

    public void Purge(ISketch component)
    {
        _sketches.Remove(component);
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

    public bool Pick(Ray ray, out IEnumerable<ISketch> sketches)
    {
        var intersectedSketches = new List<ISketch>();
        var found = false;

        foreach (var item in _sketches)
        {
            var aabb = item.BoundingBox;
            if (Collision.RayIntersectsBox(ref ray, ref aabb, out float _))
            {
                intersectedSketches.Add(item);
                found = true;
            }
        }

        sketches = intersectedSketches;
        return found;
    }
    public IReadOnlyCollection<ISketch> Sketches => _sketches;

    public Vector3[] GetVertices()
    {
        var sketches = new List<Vector3>();
        for (var i = 0; i < _sketches.Count; i++)
        {
            var sketch = _sketches[i];
            sketches.AddRange(sketch.Vertices);
        }

        // draw bbs
        for (var i = 0; i < _sketches.Count; i++)
        {
            var sketch = _sketches[i];
            sketches.AddRange(GenerateBoxLines(sketch.BoundingBox.Minimum, sketch.BoundingBox.Maximum));
        }
        
        return sketches.ToArray();
    }

    public Color[] GetColors()
    {
        var vertex = new List<Color>();
        for (var i = 0; i < _sketches.Count; i++)
        {
            vertex.Add(_colors[i]);
        }

        // // draw bbs
        // foreach (var sketch in _sketches)
        // {
        //     vertex.AddRange(GenerateBoxLines(sketch.BoundingBox.Minimum, sketch.BoundingBox.Maximum));
        // }
        
        return vertex.ToArray();
    }


    private static Vector3[] GenerateBoxLines(Vector3 min, Vector3 max)
    {
        var vertices = new Vector3[8];
        vertices[0] = new Vector3(min.X, min.Y, min.Z);
        vertices[1] = new Vector3(max.X, min.Y, min.Z);
        vertices[2] = new Vector3(max.X, max.Y, min.Z);
        vertices[3] = new Vector3(min.X, max.Y, min.Z);
        vertices[4] = new Vector3(min.X, min.Y, max.Z);
        vertices[5] = new Vector3(max.X, min.Y, max.Z);
        vertices[6] = new Vector3(max.X, max.Y, max.Z);
        vertices[7] = new Vector3(min.X, max.Y, max.Z);
        
        var lines = new[]
        {
            // Bottom face
            vertices[0], vertices[1],
            vertices[1], vertices[2],
            vertices[2], vertices[3],
            vertices[3], vertices[0],

            // Top face
            vertices[4], vertices[5],
            vertices[5], vertices[6],
            vertices[6], vertices[7],
            vertices[7], vertices[4],

            // Vertical edges
            vertices[0], vertices[4],
            vertices[1], vertices[5],
            vertices[2], vertices[6],
            vertices[3], vertices[7]
        };

        return lines;
    }
}