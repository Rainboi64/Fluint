// 
// IRenderer.cs
// 
// Copyright (C) 2022 Yaman Alhalabi

using System;
using Fluint.Layer.Graphics.Common;
using Fluint.Layer.Mathematics;

namespace Fluint.Layer.Graphics.Renderers;

public interface IRenderer : IDisposable
{
    public ModelViewProjection WorldView
    {
        get;
        set;
    }

    public Viewport Viewport
    {
        get;
        set;
    }

    void Start();
    void PreRender();
    void Render();
    void PostRender();
}