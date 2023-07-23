// 
// GizmoCylinder.cs
// 
// Copyright (C) 2022 Yaman Alhalabi

using System;
using System.Collections.Generic;
using Fluint.Layer.Editor.Gizmos;
using Fluint.Layer.Graphics.API;
using Fluint.Layer.Mathematics;

namespace Fluint.Editor.Base.Gizmos;

public class GizmoCylinder : IGizmoCylinder
{
    public PositionColorVertex[] GenerateVertex()
    {
        var points = new List<Vector3>();
        var color = (Vector4)Color;
        var center = new PositionColorVertex(Center, color);
        var offset = Vector3.UnitZ * Height;
        var tip = new PositionColorVertex(Center + offset, color);

        var angle = MathF.Tau / BaseSegments;

        for (var i = 0; i < BaseSegments; i += 1)
        {
            var n = i % BaseSegments * angle;
            var point =
                new Vector3(
                    Center.X + MathF.Cos(n) * Radius,
                    Center.Y + MathF.Sin(n) * Radius,
                    Center.Z);
            points.Add(point);
        }

        if (IsCone)
        {
            var count = points.Count;
            var vertices = new List<PositionColorVertex>();
            for (var i = 0; i < count; i++)
            {
                vertices.Add(new PositionColorVertex(points[i % count], color));
                vertices.Add(center);
                vertices.Add(new PositionColorVertex(points[(i + 1) % count], color));

                vertices.Add(new PositionColorVertex(points[i % count], color));
                vertices.Add(tip);
                vertices.Add(new PositionColorVertex(points[(i + 1) % count], color));
            }

            return vertices.ToArray();
        }
        else
        {
            var count = points.Count;
            var vertices = new List<PositionColorVertex>();
            for (var i = 0; i < count; i++)
            {
                // Bottom
                vertices.Add(new PositionColorVertex(points[i % count], color));
                vertices.Add(center);
                vertices.Add(new PositionColorVertex(points[(i + 1) % count], color));

                // Top
                vertices.Add(new PositionColorVertex(points[i % count] + offset, color));
                vertices.Add(tip);
                vertices.Add(new PositionColorVertex(points[(i + 1) % count] + offset, color));

                // Sides
                vertices.Add(new PositionColorVertex(points[i % count], color));
                vertices.Add(new PositionColorVertex(points[(i + 1) % count], color));
                vertices.Add(new PositionColorVertex(points[i % count] + offset, color));

                vertices.Add(new PositionColorVertex(points[i % count] + offset, color));
                vertices.Add(new PositionColorVertex(points[(i + 1) % count] + offset, color));
                vertices.Add(new PositionColorVertex(points[(i + 1) % count], color));
            }

            return vertices.ToArray();
        }
    }

    public bool IsCone
    {
        get;
        set;
    }

    public Vector3 Center
    {
        get;
        set;
    }

    public int BaseSegments
    {
        get;
        set;
    }

    public float Height
    {
        get;
        set;
    }

    public float Radius
    {
        get;
        set;
    }

    public Color Color
    {
        get;
        set;
    }
}