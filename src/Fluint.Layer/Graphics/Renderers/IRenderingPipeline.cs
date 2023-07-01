// 
// IRenderingPipeline.cs
// 
// Copyright (C) 2022 Yaman Alhalabi

using System.Collections.Generic;

namespace Fluint.Layer.Graphics.Renderers;

[Initialization(InitializationMethod.Scoped)]
public interface IRenderingPipeline : IModule
{
    public ICollection<IRenderer> Renderers
    {
        get;
    }

    public void Start();
    public void PreRender();
    public void Render();
    public void PostRender();
}