//
// IRenderable3D.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//


using Fluint.Layer.Mathematics;

namespace Fluint.Layer.Graphics
{
    [Initialization(InitializationMethod.Scoped)]
    public interface IRenderable3D<TVertexType> : IModule where TVertexType : struct
    {
        public uint[] Indices
        {
            get;
            set;
        }

        public TVertexType[] Vertices
        {
            get;
            set;
        }

        public ShaderPacket Packet
        {
            get;
            set;
        }

        public Matrix ModelMatrix
        {
            get;
            set;
        }

        public Vector3 Translation
        {
            get;
            set;
        }

        public Vector3 Scale
        {
            get;
            set;
        }

        public Quaternion Rotation
        {
            get;
            set;
        }
    }
}