// 
// ResizeEvemt.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using System;
using Fluint.Layer.Mathematics;

namespace Fluint.Layer.Windowing;

public class ResizeEvent : EventArgs
{
    public ResizeEvent(Vector2i size)
    {
        Size = size;
    }

    public Vector2i Size
    {
        get;
    }
}