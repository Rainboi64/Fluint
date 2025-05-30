//
// Polygon.cs
//
// Copyright (C) 2023 Yaman Alhalabi

using System;
using System.Collections.Generic;
using Fluint.Layer.Editor.Tools.Sketching.Shapes;
using Fluint.Layer.EntityComponentSystem;
using Fluint.Layer.Mathematics;

namespace Fluint.Editor.Base.Tools.Sketching.Shapes;

public class Polygon : IPolygon
{
    private Vector3 _center = Vector3.Zero;
    private Vector3 _corner = Vector3.One;
    private float _depth = 0;
    private int _segments = 16;
    private Vector3 _up = Vector3.Up;

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

    public Vector3[] Vertices
    {
        get;
        private set;
    }

    public int Segments
    {
        get => _segments;
        set
        {
            _segments = Math.Max(value, 1);
            Update();
        }
    }

    public float Depth
    {
        get => _depth;
        set {
            _depth = value;
            Update();
        }
    }

    public Vector3 Center
    {
        get => _center;
        set
        {
            _center = value;
            Update();
        }
    }

    public Vector3 Corner
    {
        get => _corner;
        set
        {
            _corner = value;
            Update();
        }
    }

    public Vector3 Up
    {
        get => _up;
        set
        {
            _up = value;
            Update();
        }
    }

    private void Update()
    {
        
        if (_segments == 1)
        {
            Vertices = new[]
                { _center, _corner };
            BoundingBox = Layer.Mathematics.BoundingBox.FromPoints(Vertices);

            return;
        }

        var points = new List<Vector3>();
        var angle = MathF.Tau / _segments;

        var a0 = angle / (1.0f + _segments % 2);

        for (var i = 0; i < _segments; i++)
        {
            var n = a0 + i % _segments * angle;
            var point = _center + Vector3.Transform(_center - _corner, Quaternion.RotationAxis(_up, n));
            points.Add(point);

        }

        var count = points.Count;
        var vertices = new List<Vector3>();
        for (var i = 0; i < count; i += 1)
        {
            if(_depth != 0) {
                vertices.Add(points[i % count]);
                vertices.Add(points[i % count] + _up * _depth);

                vertices.Add(points[(i + 1) % count]);
                vertices.Add(points[(i + 1) % count] + _up * _depth);

                vertices.Add(points[i % count]);
                vertices.Add(points[(i + 1) % count]);

                vertices.Add(points[i % count] + _up * _depth);
                vertices.Add(points[(i + 1) % count] + _up * _depth);
            }

            vertices.Add(points[i % count]);
            vertices.Add(points[(i + 1) % count]);
        }

        Vertices = vertices.ToArray();
        UpdateBoundingBox();
    }
    
    
    public void UpdateBoundingBox()
    {
        var bb = BoundingBox.FromPoints(Vertices);
        
        const float padding = 0.1f;
        var b = Vector3.One * padding;
        
        var min = bb.Minimum;
        var max = bb.Maximum + b;

        BoundingBox = new BoundingBox(min, max);
    }

}
