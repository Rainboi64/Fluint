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
    public readonly int Stride;

    public GLVertexAttribute(string name, int index, VertexAttribType type, int components, int offset)
    {
        Name = name;
        Index = index;
        Type = type;
        Components = components;
        Offset = offset;

        switch (type)
        {
            case VertexAttribType.Float:
                Stride = sizeof(float);
                break;
            
            case VertexAttribType.UnsignedByte:
            case VertexAttribType.Byte:
                Stride = sizeof(byte);
                break;
            
            case VertexAttribType.UnsignedInt:
            case VertexAttribType.Int:
            case VertexAttribType.Fixed:
            case VertexAttribType.UnsignedInt2101010Rev:
            case VertexAttribType.UnsignedInt10F11F11FRev:
            case VertexAttribType.Int2101010Rev:
                Stride = sizeof(int);
                break;
            
            case VertexAttribType.UnsignedShort:
            case VertexAttribType.Short:
                Stride = sizeof(short);
                break;
            
            case VertexAttribType.Double:
                Stride = sizeof(double);
                break;
            
            case VertexAttribType.HalfFloat:
                Stride = sizeof(float) / 2;
                break;

            default:
                Stride = 0;
                break;
        }
    }
}