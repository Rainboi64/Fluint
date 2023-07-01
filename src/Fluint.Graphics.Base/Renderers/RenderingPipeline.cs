// 
// RenderingPipeline.cs
// 
// Copyright (C) 2022 Yaman Alhalabi

using System.Collections.Generic;
using Fluint.Layer.Graphics.Renderers;

namespace Fluint.Graphics.Base.Renderers;

public class RenderingPipeline : IRenderingPipeline
{
    public ICollection<IRenderer> Renderers
    {
        get;
    } = new List<IRenderer>();

    public void Start()
    {
        foreach (var renderer in Renderers)
        {
            renderer.Start();
        }
    }

    public void PreRender()
    {
        foreach (var renderer in Renderers)
        {
            renderer.PreRender();
        }
    }

    public void Render()
    {
        foreach (var renderer in Renderers)
        {
            renderer.Render();
        }
    }

    public void PostRender()
    {
        foreach (var renderer in Renderers)
        {
            renderer.PostRender();
        }
    }
}