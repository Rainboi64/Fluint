// 
// DebugRenderServer.cs
// 
// Copyright (C) 2022 Yaman Alhalabi

using System;
using Fluint.Layer.Graphics.API;
using Fluint.Layer.Mathematics;

namespace Fluint.Layer.Graphics.Renderers;

[Initialization(InitializationMethod.Singleton)]
public interface IDebugRenderServer : IModule
{
    PositionColorVertex[] DebugVertex
    {
        get;
    }

    void DrawLine(Vector3 start, Vector3 end, Color color);
    void DrawRay(Ray ray, float length, Color color);

    event EventHandler OnVertexChanged;
}