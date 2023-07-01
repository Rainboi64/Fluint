//
// GL46Shader.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System.IO;
using Fluint.Layer.Diagnostics;
using Fluint.Layer.Graphics.API;
using OpenTK.Graphics.OpenGL4;

namespace Fluint.Graphics.API.GL46;

internal class GL46Shader : Shader
{
    private readonly GL46GraphicsFactory _graphicsFactory;
    private readonly ILogger _logger;
    private int _nativeShader;

    public GL46Shader(GL46GraphicsFactory graphicsFactory, ILogger logger)
    {
        _logger = logger;
        _graphicsFactory = graphicsFactory;
    }

    public static implicit operator int(GL46Shader shader)
    {
        return shader._nativeShader;
    }

    protected override void CompileFileInternal(ShaderStage shaderStage, string filePath, VertexType vertexType)
    {
        var shaderText = File.ReadAllText(filePath);
        CompileStringInternal(shaderStage, shaderText, vertexType);
    }

    protected override void CompileStringInternal(ShaderStage shaderStage, string shaderText, VertexType vertexType)
    {
        _nativeShader = GL.CreateShaderProgram(shaderStage.ToOpenTK(), 1, new[] { shaderText });
        ValidateProgram();

        if (shaderStage == ShaderStage.Vertex && vertexType != VertexType.Unknown)
        {
            InputLayout = _graphicsFactory.CreateInputLayout(vertexType);
        }
    }

    public override void Dispose()
    {
        GL.DeleteProgram(_nativeShader);
    }

    private void ValidateProgram()
    {
        GL.ProgramParameter(_nativeShader, ProgramParameterName.ProgramSeparable, 1);
        GL.GetProgram(_nativeShader, GetProgramParameterName.LinkStatus, out var compiled);
        if (compiled == 0)
        {
            GL.GetProgramInfoLog(_nativeShader, out var programLog);
            _logger.Error("[{0}]: {1}", "GL46Shader", programLog);
            GL.DeleteShader(_nativeShader);
        }
    }
}