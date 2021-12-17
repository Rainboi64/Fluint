//
// PositionNormalVertex.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using Fluint.Layer.Mathematics;

namespace Fluint.Layer.Graphics
{
    /// <summary>
    /// A data structure to be loaded into buffers, contains position, and normal.
    /// </summary>
    public struct PositionNormalVertex
    {
        public readonly Vector3 Position { get; }
        public readonly Vector3 Normal { get; }

        public PositionNormalVertex(Vector3 position, Vector3 normal) 
        {
            Position = position;
            Normal = normal;
        }
    }
}
