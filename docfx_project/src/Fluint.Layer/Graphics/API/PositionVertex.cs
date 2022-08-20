//
// PositionVertex.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using Fluint.Layer.Mathematics;

namespace Fluint.Layer.Graphics.API
{
    /// <summary>
    /// A data structure to be loaded into buffers, contains position.
    /// </summary>
    public struct PositionVertex
    {
        public Vector3 Position
        {
            get;
            set;
        }

        public PositionVertex(Vector3 position)
        {
            Position = position;
        }
    }
}