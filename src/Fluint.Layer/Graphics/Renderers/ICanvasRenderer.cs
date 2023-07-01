// 
// ICanvasRenderer.cs
// 
// Copyright (C) 2022 Yaman Alhalabi

using Fluint.Layer.Graphics.API;

namespace Fluint.Layer.Graphics.Renderers;

[Initialization(InitializationMethod.Scoped)]
public interface ICanvasRenderer : IModule
{
    ITexture Texture
    {
        get;
    }

    ICanvas Canvas
    {
        set;
        get;
    }

    void Update();
}