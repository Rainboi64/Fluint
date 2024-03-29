// 
// GL46VertexBuffer.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using System.Runtime.InteropServices;
using Fluint.Layer.Graphics.API;
using OpenTK.Graphics.OpenGL4;

namespace Fluint.Graphics.API.GL46;

public class GL46VertexBuffer : IVertexBuffer
{
    private readonly int _handle;

    public GL46VertexBuffer()
    {
        GL.CreateBuffers(1, out _handle);
    }

    public int VertexStride
    {
        get;
        private set;
    }

    public void Dispose()
    {
        GL.DeleteBuffer(_handle);
    }

    public void Initialize<T>(T[] vertices) where T : struct
    {
        VertexStride = Marshal.SizeOf<T>();
        var size = vertices.Length * VertexStride;
        Bind();
        GL.BufferData(BufferTarget.ArrayBuffer, size, vertices, BufferUsageHint.DynamicDraw);
    }

    public static IVertexBuffer Create<T>(T[] vertices) where T : struct
    {
        var buffer = new GL46VertexBuffer();
        buffer.Initialize(vertices);
        return buffer;
    }

    public void Bind()
    {
        GL.BindBuffer(BufferTarget.ArrayBuffer, _handle);
    }

    public void Unbind()
    {
        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
    }

    public static implicit operator int(GL46VertexBuffer buffer)
    {
        return buffer._handle;
    }
}