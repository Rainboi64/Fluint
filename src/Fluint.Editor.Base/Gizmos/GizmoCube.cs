// 
// GizmoCube.cs
// 
// Copyright (C) 2023 Yaman Alhalabi

using System.Collections.Generic;
using Fluint.Layer.Editor.Gizmos;
using Fluint.Layer.Graphics.API;
using Fluint.Layer.Mathematics;

namespace Fluint.Editor.Base.Gizmos;

public class GizmoCube : IGizmoCube
{
    public PositionColorVertex[] GenerateVertex()
    {
        var corners = Box.GetCorners();
        var vertices = new List<PositionColorVertex>();

        return vertices.ToArray();
    }

    public OrientedBoundingBox Box
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