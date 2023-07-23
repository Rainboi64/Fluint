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

    public float Radius
    {
        get;
        set;
    }

    public int Sides
    {
        get;
        set;
    }

    public PositionColorVertex[] GenerateVertex()
    {
        var points = new List<Vector3>();
        var center = new PositionColorVertex(Center, Vector4.One);
        var angle = MathF.Tau / Sides;

        for (var i = 0; i < Sides; i += Skip)
        {
            var n = i % Sides * angle;
            var point =
                new Vector3(
                    Center.X + MathF.Cos(n) * Radius,
                    Center.Y + MathF.Sin(n) * Radius,
                    Center.Z);
            points.Add(point);
        }

        var count = points.Count;
        var vertices = new List<PositionColorVertex>();
        for (var i = 0; i < count; i++)
        {
            vertices.Add(new PositionColorVertex(points[i % count], Vector4.One));
            vertices.Add(center);
            vertices.Add(new PositionColorVertex(points[(i + 1) % count], Vector4.One));
        }

        return vertices.ToArray();
    }
}