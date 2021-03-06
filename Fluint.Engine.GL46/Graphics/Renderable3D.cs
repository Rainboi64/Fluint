using System;
using Fluint.Layer.Graphics;
using Fluint.Layer.Mathematics;
using OpenTK.Graphics.OpenGL4;

namespace Fluint.Engine.GL46.Graphics
{
    public class Renderable3D<VertexType> : IRenderable3D<VertexType> where VertexType : struct
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

        public uint[] Indices
        {
            get => _indices;
            set
            {
                _indices = value;
                ElementBuffer.Bind();
                GL.BufferData(BufferTarget.ArrayBuffer, Indices.Length * sizeof(uint), value, BufferUsageHint.DynamicDraw);
            }
        }

        public VertexType[] Vertices
        {
            get => _vectors;
            set
            {
                _vectors = value;
                VertexBuffer.Bind();
                GL.BufferData(BufferTarget.ArrayBuffer, MeshVertex.Size * value.Length, value, BufferUsageHint.DynamicDraw);
            }
        }

        private Matrix _modelMatrix;
        private Vector3 _translation;
        private Vector3 _scale;
        private Quaternion _rotation;

        public Matrix ModelMatrix
        {
            get => _modelMatrix;
            set
            {
                _modelMatrix = value;
                _modelMatrix.Decompose(out _scale, out _rotation, out _translation);
            }
        }
        public Vector3 Translation
        {
            get => _translation;
            set
            {
                _translation = value;
                _modelMatrix.TranslationVector = value;
            }
        }
        public Vector3 Scale
        {
            get => _scale;
            set 
            {
                _scale = value;
                _modelMatrix.ScaleVector = value;
            }
        }
        public Quaternion Rotation
        {
            get => _rotation;
            set 
            {
                // yep, the same old s*r*t
                _modelMatrix = Matrix.Scaling(_scale) * Matrix.RotationQuaternion(value) * Matrix.Translation(_translation);
            }
        }
        public ShaderPacket Packet { get; set; }
    }
}

