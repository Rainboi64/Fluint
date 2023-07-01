// 
// DebugRenderServer.cs
// 
// Copyright (C) 2022 Yaman Alhalabi

using System;
using System.Collections.Generic;
using Fluint.Layer.Graphics.API;
using Fluint.Layer.Graphics.Renderers;
using Fluint.Layer.Mathematics;

namespace Fluint.Graphics.Base.Renderers;

public class DebugRenderServer : IDebugRenderServer
{
    private readonly List<PositionColorVertex> _vertices = new();
    public event EventHandler OnVertexChanged;

    public void DrawRay(Ray ray, float length, Color color)
    {
        _vertices.Add(new PositionColorVertex(ray.Position, color.ToVector4()));
        _vertices.Add(new PositionColorVertex(ray.Position + ray.Direction * length, color.ToVector4()));

        OnVertexChanged?.Invoke(this, EventArgs.Empty);
    }


    public void DrawLine(Vector3 start, Vector3 end, Color color)
    {
        _vertices.Add(new PositionColorVertex(start, color.ToVector4()));
        _vertices.Add(new PositionColorVertex(end, color.ToVector4()));

        OnVertexChanged?.Invoke(this, EventArgs.Empty);
    }

    public PositionColorVertex[] DebugVertex => _vertices.ToArray();
}