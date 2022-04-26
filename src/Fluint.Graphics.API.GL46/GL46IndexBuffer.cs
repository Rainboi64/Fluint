// 
// GL46IndexBuffer.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using System;
using System.Runtime.InteropServices;
using Fluint.Layer.Graphics.API;
using OpenTK.Graphics.OpenGL4;

namespace Fluint.Engine.GL46;

public class GL46IndexBuffer : IIndexBuffer
{
    private readonly int _handle;

    private GL46IndexBuffer()
    {
        GL.CreateBuffers(1, out _handle);
    }

    public bool Is16Bit
    {
        get;
        private set;
    }

    public void Dispose()
    {
        GL.DeleteBuffer(_handle);
    }

    public static IIndexBuffer Create<T>(T[] indices) where T : struct
    {
        if (typeof(T) != typeof(ushort) || typeof(T) != typeof(uint))
        {
            throw new InvalidOperationException();
        }

        var buffer = new GL46IndexBuffer();
        buffer.Initialize(indices);
        buffer.Is16Bit = typeof(T) == typeof(ushort);
        return buffer;
    }

    public static implicit operator int(GL46IndexBuffer indexBuffer)
    {
        return indexBuffer._handle;
    }

    private void Initialize<T>(T[] indices) where T : struct
    {
        var size = indices.Length * Marshal.SizeOf<T>();
        GL.NamedBufferStorage(_handle, size, indices, BufferStorageFlags.DynamicStorageBit);
    }
}