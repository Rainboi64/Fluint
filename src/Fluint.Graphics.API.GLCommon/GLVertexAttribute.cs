// 
// GLVertexAttribute.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using OpenTK.Graphics.OpenGL4;

namespace Fluint.Graphics.API.GLCommon;

public readonly struct GLVertexAttribute
{
    public readonly string Name;
    public readonly int Index;
    public readonly VertexAttribType Type;
    public readonly int Components;
    public readonly int Offset;

    public GLVertexAttribute(string name, int index, VertexAttribType type, int components, int offset)
    {
        Name = name;
        Index = index;
        Type = type;
        Components = components;
        Offset = offset;
    }
}