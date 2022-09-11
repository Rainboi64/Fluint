// 
// IMesh.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using Fluint.Layer.Graphics.API;

namespace Fluint.Layer.Graphics;

[Initialization(InitializationMethod.Scoped)]
public interface IMesh : IModule
{
    IVertexBuffer VertexBuffer
    {
        get;
    }
}