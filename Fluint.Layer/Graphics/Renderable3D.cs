//
// IRenderable3D.cs
//
// Copyright (C) 2020 Yaman Alhalabi
//


using Fluint.Layer.Mathematics;

namespace Fluint.Layer.Graphics
{
    public class Renderable3D<VertexType> where VertexType : struct
    {
        private bool _modified = true;
        private Matrix _modelTransform;
        private Vector3 _translation;
        private Vector3 _scale;
        private Quaternion _rotation;

        public uint[] Indices { get; set; }
        public VertexType[] Vertices { get; set; }

        public Matrix ModelMatrix
        {
            get
            {
                if (!_modified)
                {
                    return _modelTransform;
                }
                _modified = false;
                _modelTransform = Matrix.Scaling(_scale);
                _modelTransform *= Matrix.RotationQuaternion(_rotation);
                _modelTransform *= Matrix.Translation(_translation);

                return _modelTransform;
            }
            set
            {
                _modelTransform = value;
                _translation = value.TranslationVector;
                _rotation = Quaternion.RotationMatrix(value);
                _scale = value.ScaleVector;
            }
        }

        public Vector3 Translation
        {
            get => _translation;
            set
            {
                _translation = value;
                _modified = true;
            }
        }
        public Vector3 Scale
        {
            get => _scale;
            set
            {
                _scale = value;
                _modified = true;
            }
        }
        public Quaternion Rotation
        {
            get => _rotation;
            set
            {
                _rotation = value;
                _modified = true;
            }
        }

        public Renderable3D() { }
        public Renderable3D(VertexType[] vertexArray, uint[] indices, Matrix transform)
        {
            Vertices = vertexArray;
            Indices = indices;
            ModelMatrix = transform;
        }
    }
}