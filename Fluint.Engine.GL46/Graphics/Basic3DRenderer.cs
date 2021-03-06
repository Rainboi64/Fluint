//
// Fluint3DRenderer.cs
//
// Copyright (C) 2020 Yaman Alhalabi
//

using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;
using Fluint.Layer.Graphics;
using Fluint.Layer.Mathematics;

namespace Fluint.Engine.GL46.Graphics
{
    public class Basic3DRenderer<VertexType> : IRenderer3D<VertexType> where VertexType : struct
    {
        private List<Renderable3D<VertexType>> _renderables;
        private IVertexLayout<VertexType> _layout;
        private IShader _shader;

        public void Begin(IVertexLayout<VertexType> layout, IShader shader)
        {
            _renderables = new List<Renderable3D<VertexType>>();
            _shader = shader;
            _layout = layout;
            _layout.Calculate();
        }

        public void End()
        {
            _renderables = null;
        }

        public void Flush()
        {
            _shader.Enable();
            _layout.Load();
            foreach (var renderable in _renderables)
            {
                _shader.SetModelMatrix(renderable.ModelMatrix);
                _shader.LoadPacket(renderable.Packet);
                renderable.VertexBuffer.Bind();
                renderable.ElementBuffer.Bind();
                GL.DrawElements(PrimitiveType.Triangles, renderable.Indices.Length, DrawElementsType.UnsignedInt, 0);
            }
        }

        public void Load()
        {
        }

        /// <summary>
        /// Submits Renderables
        /// </summary>
        public void Submit(IRenderable3D<VertexType> renderable)
        {
            var newRenderable = new Renderable3D<VertexType>(renderable.Vertices, renderable.Indices, Matrix.Identity);
            newRenderable.VertexBuffer.Bind();
            GL.BufferData(newRenderable.VertexBuffer.BufferType, _layout.VertexSize * newRenderable.Vertices.Length, newRenderable.Vertices, BufferUsageHint.DynamicDraw);

            newRenderable.ElementBuffer.Bind();
            GL.BufferData(newRenderable.ElementBuffer.BufferType, _layout.VertexSize * newRenderable.Indices.Length, newRenderable.Indices, BufferUsageHint.DynamicDraw);

            _layout.Enable();

            _renderables.Add(newRenderable);
        }
    }
}
