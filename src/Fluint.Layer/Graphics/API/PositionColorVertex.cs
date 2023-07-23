//
// PositionColorVertex.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System.Runtime.InteropServices;
using Fluint.Layer.Mathematics;

namespace Fluint.Layer.Graphics.API;

/// <summary>
///     A data structure to be loaded into buffers, contains position, and color.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct PositionColorVertex
{
    public Vector3 Position;
    public Vector4 Color;

    public PositionColorVertex(Vector3 position, Vector4 color)
    {
        Position = position;
        Color = color;
    }
}