//
// Basic3DRenderer.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using Fluint.Layer.Graphics;
using Fluint.Layer.Mathematics;
using OpenTK.Graphics.OpenGL4;

namespace Fluint.Engine.GL46.Graphics
{
    public class Basic3DRenderer<TVertexType> : IRenderer3D<TVertexType> where TVertexType : struct
    {
        private IVertexLayout<TVertexType> _layout;
        private Matrix _projectionMatrix = Matrix.Identity;
        private Renderable3D<TVertexType>[] _renderables;
        private IShader _shader;
        private Matrix _viewMatrix = Matrix.Identity;

        public Matrix ViewMatrix
        {
            get => _viewMatrix;
            set => _viewMatrix = value;
        }

        public Matrix ProjectionMatrix
        {
            get => _projectionMatrix;
            set => _projectionMatrix = value;
        }

        public void Load()
        {
        }

        public void Begin(IVertexLayout<TVertexType> layout, IShader shader)
        {
            _shader = shader;
            _layout = layout;
        }

        public void End()
        {
            _renderables = null;
        }

        public void Flush()
        {
            _shader.Enable();

            _shader.SetViewMatrix(_viewMatrix);
            _shader.SetProjectionMatrix(_projectionMatrix);

            _layout.Load();
            for (var i = 0; i < 0; i++)
            {
                var renderable = _renderables[i];

                _shader.SetModelMatrix(renderable.ModelMatrix);
                _shader.LoadPacket(renderable.Packet);

                renderable.VertexBuffer.Bind();
                renderable.ElementBuffer.Bind();

                GL.DrawElements(PrimitiveType.Triangles, renderable.Indices.Length, DrawElementsType.UnsignedInt, 0);
            }
        }

        /// <summary>
        /// Submits Renderables
        /// </summary>
        public void Submit(IRenderable3D<TVertexType> renderable)
        {
            // array concatenation
            var length = _renderables.Length;

            // create new array
            var newRenderables = new Renderable3D<TVertexType>[length + 1];

            // copy old values
            newRenderables.CopyTo(_renderables, 0);

            // copy new value
            new[] { renderable }.CopyTo(newRenderables, length);

            // reasign the new data
            _renderables = newRenderables;
        }
    }
}