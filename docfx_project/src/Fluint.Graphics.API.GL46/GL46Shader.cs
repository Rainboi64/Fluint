//
// GL46Shader.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Fluint.Graphics.API.GLCommon;
using Fluint.Layer.Diagnostics;
using Fluint.Layer.Graphics;
using Fluint.Layer.Graphics.API;
using Fluint.Layer.Mathematics;
using OpenTK.Graphics.OpenGL4;
using ShaderType = OpenTK.Graphics.OpenGL4.ShaderType;

namespace Fluint.Graphics.API.GL46;

public class GL46Shader : IShader
{
    private readonly ILogger _logger;
    private readonly Dictionary<string, int> _nameLocationCache;
    private int _handle;

    public GL46Shader(ILogger logger)
    {
        _logger = logger;
        _nameLocationCache = new Dictionary<string, int>();
    }

    public GL46Shader(string vertexPath, string fragmentPath)
    {
        _nameLocationCache = new Dictionary<string, int>();

        string vertexShaderSource;

        // Read shaders from file.

        using (var reader = new StreamReader(vertexPath, Encoding.UTF8))
        {
            vertexShaderSource = reader.ReadToEnd();
        }

        string fragmentShaderSource;

        using (var reader = new StreamReader(fragmentPath, Encoding.UTF8))
        {
            fragmentShaderSource = reader.ReadToEnd();
        }

        LoadSource(vertexShaderSource, fragmentShaderSource);
    }

    public void Enable()
    {
        GL.UseProgram(_handle);
    }

    public void Disable()
    {
        GL.UseProgram(0);
    }

    public void LoadSource(string vertexShaderSource, string pixelShaderSource)
    {
        //Create shaders

        var vertexShader = GL.CreateShader(ShaderType.VertexShader);
        GL.ShaderSource(vertexShader, vertexShaderSource);

        var fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
        GL.ShaderSource(fragmentShader, pixelShaderSource);

        GL.CompileShader(vertexShader);

        //error checking
        var shaderInfoLogVertex = GL.GetShaderInfoLog(vertexShader);
        if (shaderInfoLogVertex != string.Empty)
        {
            _logger.Error("[{0}] Vertex GL46Shader Compilation Error:  {1}", "OpenGL46", shaderInfoLogVertex);
        }

        GL.CompileShader(fragmentShader);

        var shaderInfoLogFrag = GL.GetShaderInfoLog(fragmentShader);
        if (shaderInfoLogVertex != string.Empty)
        {
            _logger.Error("[{0}] Fragment GL46Shader Compilation Error: {1}", "OpenGL46", shaderInfoLogVertex);
        }

        _handle = GL.CreateProgram();

        GL.AttachShader(_handle, vertexShader);
        GL.AttachShader(_handle, fragmentShader);

        GL.LinkProgram(_handle);

        GL.DetachShader(_handle, vertexShader);
        GL.DetachShader(_handle, fragmentShader);
        GL.DeleteShader(fragmentShader);
        GL.DeleteShader(vertexShader);
    }

    public void SetModelMatrix(Matrix matrix)
    {
        UniformMat4("ml_matrix", matrix);
    }

    public void SetViewMatrix(Matrix matrix)
    {
        UniformMat4("vw_matrix", matrix);
    }

    public void SetProjectionMatrix(Matrix matrix)
    {
        UniformMat4("pn_matrix", matrix);
    }

    public void LoadPacket(ShaderPacket packet)
    {
        // Here's a reference https://www.lighthouse3d.com/tutorials/glsl-tutorial/uniform-variables/
        var length = packet.Count;

        // we use this in glActiveTexture, and is incremented when new textures are added.
        // value starts at 33984 || 0x84C0 and increments by 1.
        var textureCount = 33984;

        for (var i = 0; i < length; i++)
        {
            var current = packet[i];
            switch (current.Type)
            {
                case ShaderObjectType.Texture:
                    var value = (ITexture)current.Value;
                    Uniform1($"{packet.Tag}.{current.Tag}", textureCount);
                    GL.ActiveTexture((TextureUnit)textureCount);
                    value.Bind();

                    textureCount++;
                    break;
                case ShaderObjectType.Double:
                    Uniform1($"{packet.Tag}.{current.Tag}", (double)current.Value);
                    break;
                case ShaderObjectType.Float:
                    Uniform1($"{packet.Tag}.{current.Tag}", (float)current.Value);
                    break;
                case ShaderObjectType.Int:
                    Uniform1($"{packet.Tag}.{current.Tag}", (int)current.Value);
                    break;
                case ShaderObjectType.Vector2:
                    Uniform2($"{packet.Tag}.{current.Tag}", (Vector2)current.Value);
                    break;
                case ShaderObjectType.Vector3:
                    Uniform3($"{packet.Tag}.{current.Tag}", (Vector3)current.Value);
                    break;
                case ShaderObjectType.Vector4:
                    Uniform4($"{packet.Tag}.{current.Tag}", (Vector4)current.Value);
                    break;
                default:
                    throw new NotImplementedException($"type {current.Type} is not implemented in this module");
            }
        }
    }

    public void Dispose()
    {
        GL.DeleteProgram(_handle);
        GC.SuppressFinalize(this);
    }

    public int GetUniformLocation(string name)
    {
        if (_nameLocationCache.ContainsKey(name))
        {
            return _nameLocationCache[name];
        }

        var value = GL.GetUniformLocation(_handle, name);
        _nameLocationCache.Add(name, value);
        return value;
    }

    public void Uniform1(string name, int a)
    {
        GL.Uniform1(GetUniformLocation(name), a);
    }

    public void Uniform1(string name, float a)
    {
        GL.Uniform1(GetUniformLocation(name), a);
    }

    public void Uniform1(string name, double a)
    {
        GL.Uniform1(GetUniformLocation(name), a);
    }

    public void Uniform2(string name, double a, double b)
    {
        GL.Uniform2(GetUniformLocation(name), a, b);
    }

    public void Uniform2(string name, int a, int b)
    {
        GL.Uniform2(GetUniformLocation(name), a, b);
    }

    public void Uniform2(string name, float a, float b)
    {
        GL.Uniform2(GetUniformLocation(name), a, b);
    }

    public void Uniform2(string name, Vector2 a)
    {
        GL.Uniform2(GetUniformLocation(name), OpenTkHelper.Vector2(a));
    }

    public void Uniform3(string name, int a, int b, int c)
    {
        GL.Uniform3(GetUniformLocation(name), a, b, c);
    }

    public void Uniform3(string name, double a, double b, double c)
    {
        GL.Uniform3(GetUniformLocation(name), a, b, c);
    }

    public void Uniform3(string name, float a, float b, float c)
    {
        GL.Uniform3(GetUniformLocation(name), a, b, c);
    }

    public void Uniform3(string name, Vector3 a)
    {
        GL.Uniform3(GetUniformLocation(name), OpenTkHelper.Vector3(a));
    }

    public void Uniform4(string name, int a, int b, int c, int d)
    {
        GL.Uniform4(GetUniformLocation(name), a, b, c, d);
    }

    public void Uniform4(string name, double a, double b, double c, double d)
    {
        GL.Uniform4(GetUniformLocation(name), a, b, c, d);
    }

    public void Uniform4(string name, float a, float b, float c, float d)
    {
        GL.Uniform4(GetUniformLocation(name), a, b, c, d);
    }

    public void Uniform4(string name, Vector4 a)
    {
        GL.Uniform4(GetUniformLocation(name), OpenTkHelper.Vector4(a));
    }

    public void UniformMat3(string name, Matrix3x3 a, bool transpose = true)
    {
        var matrix = OpenTkHelper.Matrix3(a);
        GL.UniformMatrix3(GetUniformLocation(name), transpose, ref matrix);
    }

    public void UniformMat4(string name, Matrix a, bool transpose = true)
    {
        var matrix = OpenTkHelper.Matrix4(a);
        GL.UniformMatrix4(GetUniformLocation(name), transpose, ref matrix);
    }

    public static implicit operator int(GL46Shader shader)
    {
        return shader._handle;
    }
}