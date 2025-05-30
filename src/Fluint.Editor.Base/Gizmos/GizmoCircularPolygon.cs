// 
// GizmoCircularPolygon.cs
// 
// Copyright (C) 2022 Yaman Alhalabi

using System;
using System.Collections.Generic;
using Fluint.Layer.Editor.Gizmos;
using Fluint.Layer.Graphics.API;
using Fluint.Layer.Mathematics;

namespace Fluint.Editor.Base.Gizmos;

public class GizmoCircularPolygon : IGizmoCircularPolygon
{
    public int Skip
    {
        get;
        set;
    } = 1;

    public Vector3 Center
    {
        get;
        set;
    }

    public Vector3 Corner
    {
        get;
        set;
    }

    public int Sides
    {
        get;
        set;
    }

    public Color Color
    {
        get;
        set;
    }

    public PositionWidthColorVertex[] GetVertices()
    {
        var points = new List<Vector3>();
        var angle = MathF.Tau / Sides;

        var a0 = angle / (1.0f + Sides % 2);

        for (var i = 0; i < Sides; i++)
        {
            var n = a0 + i % Sides * angle;
            var point = Center + Vector3.Transform(Center - Corner, Quaternion.RotationAxis(Vector3.Cross(Center, Corner), n));
            points.Add(point);
        }

        var count = points.Count;
        var vertices = new List<PositionWidthColorVertex>();
        for (var i = 0; i < count; i += 1)
        {
            vertices.Add(new PositionWidthColorVertex(points[i % count], 1, Color.ToVector4()));
            vertices.Add(new PositionWidthColorVertex(points[(i + 1) % count], 1, Color.ToVector4()));
        }

        return vertices.ToArray();
    }
}