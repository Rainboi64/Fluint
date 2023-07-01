// 
// Grid.cs
// 
// Copyright (C) 2022 Yaman Alhalabi

using Fluint.Layer.Mathematics;

namespace Fluint.Layer.Editor;

public struct Grid
{
    public Grid(Vector2i offsets, Vector2i size)
    {
        Offsets = offsets;
        Size = size;
    }

    public Vector2i Offsets
    {
        get;
    }

    public Vector2i Size
    {
        get;
    }
}