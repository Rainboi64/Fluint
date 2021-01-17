using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using Fluint.Layer.Graphics;
using Fluint.Layer.Mathematics;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using SixLabors.ImageSharp.Processing;

namespace Fluint.Engine.GL46.Graphics
{
    public class Renderable3D<VertexType> : Layer.Graphics.Renderable3D<VertexType> where VertexType : struct
    {
        private uint[] _indices;
        private VertexType[] _vectors;
        private Buffer _vertexBuffer;
        private Buffer _elementBuffer;

        public Renderable3D(VertexType[] vertices, uint[] indices, Matrix model)
        {
            Vertices = vertices;
            Indices = indices;
        }

        public Buffer VertexBuffer
        {
            get
            {
                if (_vertexBuffer is null)
                {
                    _vertexBuffer = new Buffer(BufferTarget.ArrayBuffer);
                }

                return _vertexBuffer;
            }
        }
        public Buffer ElementBuffer
        {
            get
            {
                if (_elementBuffer is null)
                    _elementBuffer = new Buffer(BufferTarget.ElementArrayBuffer);
                return _elementBuffer;
            }
        }

        public new uint[] Indices
        {
            get => _indices;
            set
            {
                _indices = value;
                ElementBuffer.Bind();
                GL.BufferData(BufferTarget.ArrayBuffer, Indices.Length * sizeof(uint), value, BufferUsageHint.DynamicDraw);
            }
        }

        public new VertexType[] Vertices
        {
            get => _vectors;
            set
            {
                var xd = value;
                _vectors = value;
                VertexBuffer.Bind();
                GL.BufferData(BufferTarget.ArrayBuffer, MeshVertex.Size * value.Length, value, BufferUsageHint.DynamicDraw);
            }
        }
    }
}

