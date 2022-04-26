// 
// GL46Pipeline.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using Fluint.Layer.Graphics.API;
using Fluint.Layer.Mathematics;
using OpenTK.Graphics.OpenGL4;

namespace Fluint.Engine.GL46.Graphics;

public class GL46Pipeline : IPipeline
{
    private readonly int _handle;

    public GL46Pipeline(IShader vertexGL46Shader, IShader pixelGL46Shader, IInputLayout inputLayout, Viewport viewport,
        PrimitiveTopology primitiveTopology)
    {
        VertexShader = vertexGL46Shader;
        PixelShader = pixelGL46Shader;
        InputLayout = inputLayout;
        Viewport = viewport;
        PrimitiveTopology = primitiveTopology;

        GL.CreateProgramPipelines(1, out _handle);
        GL.UseProgramStages(_handle, ProgramStageMask.VertexShaderBit, (GL46Shader)vertexGL46Shader);
        GL.UseProgramStages(_handle, ProgramStageMask.FragmentShaderBit, (GL46Shader)pixelGL46Shader);
    }

    public PrimitiveTopology PrimitiveTopology
    {
        get;
    }

    public IShader VertexShader
    {
        get;
    }

    public IShader PixelShader
    {
        get;
    }

    public IInputLayout InputLayout
    {
        get;
    }

    public IRasterizerState RasterizerState
    {
        get;
    }

    public IDepthStencilState DepthStencilState
    {
        get;
    }

    public IBlendState BlendState
    {
        get;
    }

    public Viewport Viewport
    {
        get;
    }

    public void Dispose()
    {
        RasterizerState?.Dispose();
        DepthStencilState?.Dispose();
        BlendState?.Dispose();

        GL.DeleteProgramPipeline(_handle);
    }

    public static implicit operator int(GL46Pipeline pipeline)
    {
        return pipeline._handle;
    }
}