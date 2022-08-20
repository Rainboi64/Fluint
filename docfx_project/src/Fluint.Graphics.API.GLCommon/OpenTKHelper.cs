//
// OpenTKHelper.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using Fluint.Layer.Graphics.API;
using Fluint.Layer.Mathematics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using Color4 = OpenTK.Mathematics.Color4;
using Rectangle = System.Drawing.Rectangle;
using Vector2 = OpenTK.Mathematics.Vector2;
using Vector2i = OpenTK.Mathematics.Vector2i;
using Vector3 = OpenTK.Mathematics.Vector3;
using Vector4 = OpenTK.Mathematics.Vector4;

namespace Fluint.Graphics.API.GLCommon;

/// <summary>
///     Converts some math stuff.
/// </summary>
public static class OpenTkHelper
{
    public static unsafe Matrix4 Matrix4(Matrix matrix)
    {
        return *(Matrix4*)&matrix;
    }

    public static unsafe Matrix3 Matrix3(Matrix3x3 matrix)
    {
        return *(Matrix3*)&matrix;
    }

    public static unsafe Vector3 Vector3(Layer.Mathematics.Vector3 vector3)
    {
        return *(Vector3*)&vector3;
    }

    public static unsafe Vector4 Vector4(Layer.Mathematics.Vector4 vector4)
    {
        return *(Vector4*)&vector4;
    }

    public static unsafe Color4 Color4(Layer.Mathematics.Color4 color4)
    {
        return *(Color4*)&color4;
    }

    public static unsafe Vector2 Vector2(Layer.Mathematics.Vector2 vector2)
    {
        return *(Vector2*)&vector2;
    }

    public static unsafe Layer.Mathematics.Vector2 Vector2(Vector2 vector2)
    {
        return *(Layer.Mathematics.Vector2*)&vector2;
    }

    public static unsafe Vector2i Vector2I(Layer.Mathematics.Vector2i vector2I)
    {
        return *(Vector2i*)&vector2I;
    }

    public static unsafe Layer.Mathematics.Vector2i Vector2I(Vector2i vector2I)
    {
        return *(Layer.Mathematics.Vector2i*)&vector2I;
    }

    public static unsafe Rectangle Viewport(Viewport viewport)
    {
        return *(Rectangle*)&viewport;
    }

    public static PrimitiveType PrimitiveType(PrimitiveTopology primitiveTopology)
    {
        switch (primitiveTopology)
        {
            case PrimitiveTopology.LineList:
                return OpenTK.Graphics.OpenGL4.PrimitiveType.Lines;
            case PrimitiveTopology.TriangleList:
                return OpenTK.Graphics.OpenGL4.PrimitiveType.Triangles;

            default:
            case PrimitiveTopology.NotAssigned:
                return 0;
        }
    }
}