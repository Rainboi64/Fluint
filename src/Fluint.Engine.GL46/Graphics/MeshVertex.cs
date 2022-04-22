//
// MeshVertex.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using Fluint.Layer.Mathematics;

namespace Fluint.Engine.GL46.Graphics
{
    public struct MeshVertex
    {
        // (3 + 3 + 4 + 2) = 12 float + 1 uint 
        public const int Size = (sizeof(float) * 12) + sizeof(uint);

        public Vector3 Vector
        {
            get;
            set;
        }

        public Vector3 Normal
        {
            get;
            set;
        }

        public Vector4 Color
        {
            get;
            set;
        }

        public Vector2 TextureCoordinates
        {
            get;
            set;
        }

        public uint TextureId
        {
            get;
            set;
        }
    }
}