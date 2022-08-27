// 
// IRenderable.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using Fluint.Layer.Graphics.API;

namespace Fluint.Layer.Graphics;

public interface IRenderable
{
    VertexType Type
    {
        get;
    }

    void AddTo(IRenderer renderer);
}