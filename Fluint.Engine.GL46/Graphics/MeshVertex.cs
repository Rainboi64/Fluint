//
// MeshVertex.cs
//
// Copyright (C) 2020 Yaman Alhalabi
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using Fluint.Layer.Mathematics;
using Fluint.Layer.Graphics;
namespace Fluint.Engine.GL46.Graphics
{
    public struct MeshVertex
    {
        // (3 + 3 + 4 + 2) = 12 float + 1 uint 
        public const int Size = (sizeof(float) * 12) + sizeof(uint);

        public static readonly VertexLayoutAttribute[] Layout = new []
        {
            new VertexLayoutAttribute(VertexLayoutAttributeType.Float, 3),
            new VertexLayoutAttribute(VertexLayoutAttributeType.Float, 3),
            new VertexLayoutAttribute(VertexLayoutAttributeType.Float, 4),
            new VertexLayoutAttribute(VertexLayoutAttributeType.Float, 2),
            new VertexLayoutAttribute(VertexLayoutAttributeType.UnsignedInt, 1),
        };

        public Vector3 Vector { get; set; }
        public Vector3 Normal { get; set; }
        public Vector4 Color { get; set; }
        public Vector2 TextureCoordinates { get; set; }
        public uint TextureID { get; set; }
    }
}
