//
// PositionColorVertex.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System.Runtime.InteropServices;
using Fluint.Layer.Mathematics;

namespace Fluint.Layer.Graphics.API;

[StructLayout(LayoutKind.Sequential)]
public struct PositionWidthColorVertex
{
    public Vector3 Position;
    public float Width;
    public Vector4 Color;

    public PositionWidthColorVertex(Vector3 position, float width, Vector4 color)
    {
        Position = position;
        Width = width;
        Color = color;
    }

    public override string ToString()
    {
        return $"Position: {Position}; Color: {Color}";
    }
}