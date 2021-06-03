//
// PositionNormalUVTIDVertex.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using Fluint.Layer.Mathematics;

namespace Fluint.Layer.Graphics
{
    /// <summary>
    /// A data structure to be loaded into buffers, contains position, normal, UV, TextureID.
    /// </summary>
    public struct PositionNormalUVTIDVertex
    {
        public readonly Vector3 Position;
        public readonly Vector3 Normal;
        public readonly Vector2 UV;
        public readonly uint TID;

        public PositionNormalUVTIDVertex(Vector3 position, Vector3 normal, Vector2 uv, uint tid) 
        {
            Position = position;
            Normal = normal;
            UV = uv;
            TID = tid;
        }
    }
}
