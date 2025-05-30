// 
// GL46Pipeline.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using Fluint.Layer.Graphics.API;
using Fluint.Layer.Mathematics;
using OpenTK.Graphics.OpenGL4;

namespace Fluint.Graphics.API.GL33;

public class GL33Pipeline : IPipeline
{
    private readonly int _handle;

    public GL33Pipeline(IShader vertexGL46Shader, IShader pixelGL46Shader, IInputLayout inputLayout, Viewport viewport,
        PrimitiveTopology primitiveTopology)
    {
        VertexShader = vertexGL46Shader;
        PixelShader = pixelGL46Shader;
        InputLayout = inputLayout;
        Viewport = viewport;
        PrimitiveTopology = primitiveTopology;

        GL.GenProgramPipelines(1, out _handle);
        GL.UseProgramStages(_handle, ProgramStageMask.VertexShaderBit, (GL33Shader)vertexGL46Shader);
        GL.UseProgramStages(_handle, ProgramStageMask.FragmentShaderBit, (GL33Shader)pixelGL46Shader);
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
        set;
    }

    public void Dispose()
    {
        RasterizerState?.Dispose();
        DepthStencilState?.Dispose();
        BlendState?.Dispose();

        GL.DeleteProgramPipeline(_handle);
    }

    public static implicit operator int(GL33Pipeline pipeline)
    {
        return pipeline._handle;
    }
}