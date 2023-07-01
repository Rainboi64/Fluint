// 
// IRenderer.cs
// 
// Copyright (C) 2022 Yaman Alhalabi

using System;

namespace Fluint.Layer.Graphics.Renderers;

public interface IRenderer : IDisposable
{
    void Start();
    void PreRender();
    void Render();
    void PostRender();
}