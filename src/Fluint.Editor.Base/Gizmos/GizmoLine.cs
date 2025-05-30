// 
// GizmoLine.cs
// 
// Copyright (C) 2023 Yaman Alhalabi

using Fluint.Layer.Editor.Gizmos;
using Fluint.Layer.Graphics.API;
using Fluint.Layer.Mathematics;

namespace Fluint.Editor.Base.Gizmos;

public class GizmoLine : IGizmoLine
{
    public PositionWidthColorVertex[] GetVertices()
    {
        return new PositionWidthColorVertex[] { new(Start, Width, (Vector4)Color), new(End, Width, (Vector4)Color) };
    }

    public Vector3 Start
    {
        get;
        set;
    }

    public Vector3 End
    {
        get;
        set;
    }

    public float Width
    {
        get;
        set;
    }

    public Color Color
    {
        get;
        set;
    }

    public override string ToString()
    {
        return $"Start: {Start}, end: {End}";
    }
}