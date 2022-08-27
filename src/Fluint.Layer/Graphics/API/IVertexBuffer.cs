//
//
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;

namespace Fluint.Layer.Graphics.API;

public interface IVertexBuffer : IDisposable
{
    int VertexStride
    {
        get;
    }

    void Initialize<T>(T[] vertices) where T : struct;
}