//
// Batch2DRenderer.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;
using Fluint.Layer.Graphics;
using Fluint.Layer.Mathematics;

namespace Fluint.Engine.GL46.Graphics.Renderers
{
    public struct VertexData2D
    {
        public Vector3 Vector;
        public Vector4 Color;
        public Vector2 Uv;
        public float Tid;
    }

    public class Batch2DRenderer : IRenderer2D
    {
        public void Begin()
        {
            throw new NotImplementedException();
        }

        public void End()
        {
            throw new NotImplementedException();
        }

        public void Flush()
        {
            throw new NotImplementedException();
        }

        public void Submit(IRenderable2D renderable2D)
        {
            throw new NotImplementedException();
        }
    }
}