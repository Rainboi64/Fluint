//
// PositionNormalUVTIDVertex.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using Fluint.Layer.Mathematics;

namespace Fluint.Layer.Graphics.API
{
    /// <summary>
    /// A data structure to be loaded into buffers, contains position, normal, UV, TextureID.
    /// </summary>
    public struct PositionNormalUvtidVertex
    {
        public readonly Vector3 Position;
        public readonly Vector3 Normal;
        public readonly Vector2 Uv;
        public readonly uint Tid;

        public PositionNormalUvtidVertex(Vector3 position, Vector3 normal, Vector2 uv, uint tid)
        {
            Position = position;
            Normal = normal;
            Uv = uv;
            Tid = tid;
        }
    }
}