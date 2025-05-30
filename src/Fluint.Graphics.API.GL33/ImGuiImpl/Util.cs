//
// Util.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using OpenTK.Graphics.OpenGL4;

namespace Fluint.Graphics.API.GL33.ImGuiImpl;
internal static class Util
{
    [Pure]
    public static float Clamp(float value, float min, float max)
    {
        return value < min ? min : value > max ? max : value;
    }

    public static void CheckGlError(string title)
    {
        var error = GL.GetError();
        if (error != ErrorCode.NoError)
        {
            Console.WriteLine($"{title}: {error}");
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void CreateTexture(TextureTarget target, string name, out int texture)
    {
        GL.GenTextures(1, out texture);
        // LabelObject(ObjectLabelIdentifier.Texture, texture, $"GL33Texture: {name}");
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void CreateProgram(string name, out int program)
    {
        program = GL.CreateProgram();
        // LabelObject(ObjectLabelIdentifier.Program, program, $"Program: {name}");
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void CreateShader(ShaderType type, string name, out int shader)
    {
        shader = GL.CreateShader(type);
        // LabelObject(ObjectLabelIdentifier.Shader, shader, $"GL33Shader: {type}: {name}");
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void CreateBuffer(string name, out int buffer)
    {
        GL.GenBuffers(1, out buffer);
        // LabelObject(ObjectLabelIdentifier.Buffer, buffer, $"Buffer: {name}");
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void CreateVertexBuffer(string name, out int buffer)
    {
        CreateBuffer($"VBO: {name}", out buffer);
        GL.BindBuffer(BufferTarget.ArrayBuffer, buffer);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void CreateElementBuffer(string name, out int buffer)
    {
        CreateBuffer($"EBO: {name}", out buffer);
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, buffer);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void CreateVertexArray(string name, out int vao)
    {
        GL.GenVertexArrays(1, out vao);
        GL.BindVertexArray(vao);
        // LabelObject(ObjectLabelIdentifier.VertexArray, vao, $"VAO: {name}");
    }
}
