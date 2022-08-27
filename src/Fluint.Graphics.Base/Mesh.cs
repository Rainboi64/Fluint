// 
// Mesh.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using Fluint.Layer.Graphics;
using Fluint.Layer.Graphics.API;

namespace Fluint.Graphics.Base;

public class Mesh : IMesh
{
    public Mesh(IVertexBuffer vertexBuffer)
    {
        VertexBuffer = vertexBuffer;
    }

    public IVertexBuffer VertexBuffer
    {
        get;
    }
}