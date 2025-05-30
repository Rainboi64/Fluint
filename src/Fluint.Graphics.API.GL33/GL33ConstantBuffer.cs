// 
// GL46ConstantBuffer.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using System;
using System.Runtime.InteropServices;
using Fluint.Layer.Graphics.API;
using OpenTK.Graphics.OpenGL4;

namespace Fluint.Graphics.API.GL33;

public class GL33ConstantBuffer : IConstantBuffer
{
    private readonly int _bufferSize;
    private readonly int _nativeBuffer;

    private GL33ConstantBuffer(int bufferSize)
    {
        _bufferSize = bufferSize;
        GL.CreateBuffers(1, out _nativeBuffer);
    }

    public void UpdateBuffer<T>(T data) where T : struct
    {
        GL.NamedBufferSubData(_nativeBuffer, IntPtr.Zero, _bufferSize, ref data);
    }

    public void Dispose()
    {
        GL.DeleteBuffer(_nativeBuffer);
    }

    public static implicit operator int(GL33ConstantBuffer constantBuffer)
    {
        return constantBuffer._nativeBuffer;
    }

    public static IConstantBuffer Create<T>(T data) where T : struct
    {
        var size = Marshal.SizeOf<T>();
        var buffer = new GL33ConstantBuffer(size);
        buffer.Initialize(data);
        return buffer;
    }

    private void Initialize<T>(T data) where T : struct
    {
        GL.NamedBufferStorage(_nativeBuffer, _bufferSize, ref data, BufferStorageFlags.DynamicStorageBit);
    }
}