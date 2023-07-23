// 
// Grid.cs
// 
// Copyright (C) 2022 Yaman Alhalabi

using Fluint.Layer.Mathematics;

namespace Fluint.Layer.Editor.Viewport;

public class Grid
{
    private Vector2i _offsets;
    private Vector2i _size;
    private GridType _type;

    public Grid(Vector2i offsets, Vector2i size, GridType type = GridType.XY)
    {
        _offsets = offsets;
        _size = size;
        _type = type;
        NeedsUpdate = true;
    }

    public GridType Type
    {
        get => _type;
        set
        {
            NeedsUpdate = true;
            _type = value;
        }
    }

    public Vector2i Offsets
    {
        get => _offsets;
        set
        {
            NeedsUpdate = true;
            _offsets = value;
        }
    }

    public Vector2i Size
    {
        get => _size;
        set
        {
            NeedsUpdate = true;
            _size = value;
        }
    }

    public bool NeedsUpdate
    {
        get;
        set;
    }
}