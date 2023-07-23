// 
// GL46GraphicsFactory.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using System;
using System.Collections.Generic;
using System.Linq;
using Fluint.Graphics.API.GLCommon;
using Fluint.Layer.Diagnostics;
using Fluint.Layer.Graphics.API;
using Fluint.Layer.Mathematics;
using OpenTK.Graphics.OpenGL4;

namespace Fluint.Graphics.API.GL46;

public class GL46GraphicsFactory : IGraphicsFactory
{
    private readonly GL46GraphicsDevice _graphicsDevice;
    private readonly ILogger _logger;

    public GL46GraphicsFactory(ILogger logger)
    {
        _logger = logger;
        _graphicsDevice = new GL46GraphicsDevice();
    }

    public IBlendState CreateBlendState(bool isBlendEnabled, Blend sourceBlend, Blend destinationBlend,
        BlendOperation blendOperation, Blend sourceAlphaBlend, Blend destinationAlphaBlend,
        BlendOperation blendOperationAlpha)
    {
        return null;
    }

    public ICommandList CreateCommandList()
    {
        return new GL46CommandList();
    }

    public IConstantBuffer CreateConstantBuffer<T>(T constants) where T : struct
    {
        return GL46ConstantBuffer.Create(constants);
    }

    public IDepthStencilState CreateDepthStencilState()
    {
        return null;
    }

    public IPipeline CreatePipeline(Shader vertexShader, Shader pixelShader, IInputLayout inputLayout,
        IBlendState blendState,
        IDepthStencilState depthStencilState, IRasterizerState rasterizerState, Viewport viewport,
        PrimitiveTopology primitiveTopology)
    {
        return new GL46Pipeline(vertexShader, pixelShader, inputLayout, viewport, primitiveTopology);
    }

    public IRasterizerState CreateRasterizerState(CullMode cullMode, FillMode fillMode, bool isDepthEnabled,
        bool isScissorEnabled,
        bool isMultiSampleEnabled, bool isAntialiasedLineEnabled)
    {
        return null;
    }

    public ISampler CreateSampler(TextureAddressMode addressModeU, TextureAddressMode addressModeV, Filter filter,
        ComparisonFunction comparisonFunction)
    {
        return null;
    }

    public Shader CreateShader(ShaderStage shaderStage, string shaderText, VertexType vertexType,
        IEnumerable<(string, string)> macros)
    {
        var shader = new GL46Shader(this, _logger);
        foreach (var (macroName, macroValue) in macros.ToList())
        {
            shader.AddMacro(macroName, macroValue);
        }

        shader.CompileString(shaderStage, shaderText, vertexType);
        return shader;
    }

    public Shader CreateShaderFromFile(ShaderStage shaderStage, string filePath, VertexType vertexType,
        IEnumerable<(string, string)> macros)
    {
        var shader = new GL46Shader(this, _logger);
        foreach (var (macroName, macroValue) in macros.ToList())
        {
            shader.AddMacro(macroName, macroValue);
        }

        shader.CompileFile(shaderStage, filePath + ".glsl", vertexType);
        return shader;
    }

    public ISwapChain CreateSwapchain(SwapChainDescriptor swapChainDescriptor)
    {
        return new GL46SwapChain(swapChainDescriptor);
    }

    public ITextureFactory CreateTextureFactory()
    {
        return new GL46TextureFactory();
    }

    public IVertexBuffer CreateVertexBuffer<T>(T[] vertices) where T : struct
    {
        return GL46VertexBuffer.Create(vertices);
    }

    public IIndexBuffer CreateIndexBuffer<T>(T[] indices) where T : struct
    {
        return GL46IndexBuffer.Create(indices);
    }

    public void Dispose()
    {
        _graphicsDevice?.Dispose();
    }

    internal IInputLayout CreateInputLayout(VertexType vertexType)
    {
        var attributes = new List<GLVertexAttribute>();

        switch (vertexType)
        {
            case VertexType.Position:
                attributes.Add(new GLVertexAttribute("i_position", 0, VertexAttribType.Float, 3, 0));
                break;
            case VertexType.PositionColor:
                attributes.Add(new GLVertexAttribute("i_position", 0, VertexAttribType.Float, 3, 0));
                attributes.Add(new GLVertexAttribute("i_color", 1, VertexAttribType.Float, 4, 12));
                break;
            case VertexType.PositionTexture:
                attributes.Add(new GLVertexAttribute("i_position", 0, VertexAttribType.Float, 3, 0));
                attributes.Add(new GLVertexAttribute("i_uv", 1, VertexAttribType.Float, 2, 12));
                break;
            case VertexType.PositionTextureNormalTangent:
                attributes.Add(new GLVertexAttribute("i_position", 0, VertexAttribType.Float, 3, 0));
                attributes.Add(new GLVertexAttribute("i_uv", 1, VertexAttribType.Float, 2, 12));
                attributes.Add(new GLVertexAttribute("i_normal", 2, VertexAttribType.Float, 3, 20));
                attributes.Add(new GLVertexAttribute("i_tangent", 3, VertexAttribType.Float, 3, 32));
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(vertexType), vertexType, null);
        }

        return new GL46InputLayout(attributes);
    }
}