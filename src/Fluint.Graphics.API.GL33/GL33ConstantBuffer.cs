using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Fluint.Layer.Diagnostics;
using Fluint.Layer.Graphics.API;
using OpenTK.Graphics.OpenGL;

namespace Fluint.Graphics.API.GL33;

public class GL33ConstantBuffer : IConstantBuffer
{
    private readonly int _bufferSize;
    private int _nativeBuffer;

    private GL33ConstantBuffer(int bufferSize)
    {
        _bufferSize = bufferSize;
        GL.GenBuffers(1, out _nativeBuffer);
    }

    public void UpdateBuffer<T>(T data) where T : struct
    {
        GL.BindBuffer(BufferTarget.UniformBuffer, _nativeBuffer);
        var ptr = GL.MapBuffer(BufferTarget.UniformBuffer, BufferAccess.WriteOnly);

        if (ptr != IntPtr.Zero)
        {
            Marshal.StructureToPtr(data, ptr, false);
            GL.UnmapBuffer(BufferTarget.UniformBuffer);
        }
        else
        {
            throw new EngineApiException("GL33", "Uninitialized Constant Buffer");
        }

        GL.BindBuffer(BufferTarget.UniformBuffer, 0);
        GL.Finish();
    }

    public void Dispose()
    {
        GL.DeleteBuffers(1, ref _nativeBuffer);
    }

    public static implicit operator int(GL33ConstantBuffer constantBuffer)
    {
        return constantBuffer._nativeBuffer;
    }

    public static IConstantBuffer Create<T>(T data) where T : struct
    {
        var size = Marshal.SizeOf(data);
        var buffer = new GL33ConstantBuffer(size);
        buffer.Initialize(data);
        return buffer;
    }

    private void Initialize<T>(T data) where T : struct
    {
        GL.BindBuffer(BufferTarget.UniformBuffer, _nativeBuffer);
        GL.BufferData(BufferTarget.UniformBuffer, Marshal.SizeOf(data), IntPtr.Zero, BufferUsageHint.StaticDraw);
        UpdateBuffer(data);
    }
}