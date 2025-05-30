// 
// GizmoRenderer.cs
// 
// Copyright (C) 2022 Yaman Alhalabi

using System;
using System.Collections.Generic;
using Fluint.Layer.Editor.Gizmos;
using Fluint.Layer.Graphics.API;
using Fluint.Layer.Graphics.Renderers;

namespace Fluint.Graphics.Base.Renderers;

public class GizmoRenderer : LineRenderer, IGizmoRenderer
{
    private readonly IGizmoProvider _provider;
    public GizmoRenderer(IGraphicsFactory graphicsFactory, IGizmoProvider provider) : base(graphicsFactory, "Gizmos")
    {
        _provider = provider;
    }

    public new void PreRender()
    {
        var list = new List<PositionWidthColorVertex>();
        foreach (var gizmo in _provider.Gizmos)
        {
            list.AddRange(gizmo.GetVertices());
        }

        var len = Math.Min(list.Count, MaxVertices);
        var arr = list.ToArray();
        Array.Copy(arr, BufferObject.Vertex, len);
        
        base.PreRender();
    }
}