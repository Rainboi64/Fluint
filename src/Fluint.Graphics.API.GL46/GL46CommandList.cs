//
// GL46CommandList.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;
using System.Collections.Generic;
using Fluint.Graphics.API.GLCommon;
using Fluint.Layer.Graphics.API;
using Fluint.Layer.Mathematics;
using OpenTK.Graphics.OpenGL4;

namespace Fluint.Graphics.API.GL46;

internal class GL46CommandList : ICommandList
{
    private readonly List<OpenGlCommand> _commandList;
    private int _currentInputLayout;
    private PrimitiveType _currentPrimitiveType = PrimitiveType.Triangles;

    public GL46CommandList()
    {
        _commandList = new List<OpenGlCommand>();
    }

    public void Begin(string passName, IPipeline pipeline)
    {
        var command = new OpenGlCommand
        {
            Type = CommandType.Begin,
            Name = passName
        };

        _commandList.Add(command);

        if (pipeline != null)
        {
            SetViewport(pipeline.Viewport);
            SetProgramPipeline((GL46Pipeline)pipeline);
            SetInputLayout(pipeline.InputLayout);
            SetPrimitiveTopology(pipeline.PrimitiveTopology);
        }
    }

    public void ClearRenderTarget(TextureView renderTarget, Color4 clearColor)
    {
        var command = new OpenGlCommand
        {
            Type = CommandType.ClearRenderTarget,
            ClearColor = GLExtensions.Color4(clearColor)
        };

        _commandList.Add(command);
    }

    public void Clear()
    {
        _commandList.Clear();
    }

    public void ClearDepthStencil(TextureView depthStencil, float clearDepth, byte stencilDepth)
    {
        var command = new OpenGlCommand
        {
            Type = CommandType.ClearDepthStencil,
            ClearStencil = stencilDepth,
            ClearDepth = clearDepth
        };

        _commandList.Add(command);
    }

    public void Draw(int vertexCount)
    {
        var command = new OpenGlCommand
        {
            Type = CommandType.Draw,
            DrawVertexCount = vertexCount
        };

        _commandList.Add(command);
    }

    public void DrawIndexed(int indexCount, int indexOffset, int vertexOffset)
    {
        var command = new OpenGlCommand
        {
            Type = CommandType.DrawIndexed,
            DrawIndexCount = indexCount,
            DrawIndexOffset = indexOffset,
            DrawVertexOffset = vertexOffset
        };
        _commandList.Add(command);
    }

    public void End()
    {
        var command = new OpenGlCommand
        {
            Type = CommandType.End
        };

        _commandList.Add(command);
    }

    public void SetBlendState(IBlendState blendState)
    {
        var command = new OpenGlCommand
        {
            Type = CommandType.SetBlendState
        };

        _commandList.Add(command);
    }

    public void SetConstantBuffer(IConstantBuffer buffer, BufferScope bufferScope)
    {
        var command = new OpenGlCommand
        {
            Type = CommandType.SetConstantBuffers,
            ConstantBuffer = (GL46ConstantBuffer)buffer
        };

        _commandList.Add(command);
    }

    public void SetDepthStencilState(IDepthStencilState depthStencilState)
    {
        var command = new OpenGlCommand
        {
            Type = CommandType.SetDepthStencilState
        };

        _commandList.Add(command);
    }

    public void SetIndexBuffer(IIndexBuffer indexBuffer)
    {
        var command = new OpenGlCommand
        {
            Type = CommandType.SetIndexBuffer,
            IndexBuffer = (GL46IndexBuffer)indexBuffer
        };

        _commandList.Add(command);
    }

    public void SetInputLayout(IInputLayout inputLayout)
    {
        var command = new OpenGlCommand
        {
            Type = CommandType.SetInputLayout,
            InputLayout = (GL46InputLayout)inputLayout
        };

        _commandList.Add(command);
    }

    public void SetPixelShader(IShader pixelShader)
    {
        // unused, pipeline will bind the program
    }

    public void SetPrimitiveTopology(PrimitiveTopology primitiveTopology)
    {
        var command = new OpenGlCommand
        {
            Type = CommandType.SetPrimitiveTopology,
            PrimitiveType = primitiveTopology.ToOpenTK()
        };

        _commandList.Add(command);
    }

    public void SetRasterizerState(IRasterizerState rasterizerState)
    {
        throw new NotImplementedException();
    }

    public void SetRenderTarget(TextureView renderTargets, TextureView depthStencilView)
    {
        throw new NotImplementedException();
    }

    public void SetRenderTargets(TextureView[] renderTargets, TextureView depthStencilView)
    {
        throw new NotImplementedException();
    }


    public void SetSampler(ISampler sampler)
    {
        //throw new NotImplementedException();
    }

    public void SetScissorRectangle(Rectangle rectangle)
    {
        var command = new OpenGlCommand
        {
            Type = CommandType.SetScissor,
            ScissorRectangle = new System.Drawing.Rectangle(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height)
        };

        _commandList.Add(command);
    }

    public void SetTexture(TextureView textureView)
    {
        var command = new OpenGlCommand
        {
            Type = CommandType.SetTextures,
            TextureView = (GL46TextureView)textureView
        };

        _commandList.Add(command);
    }

    public void SetVertexBuffer(IVertexBuffer vertexBuffer)
    {
        var command = new OpenGlCommand
        {
            Type = CommandType.SetVertexBuffer,
            InputLayout = _currentInputLayout,
            VertexBuffer = (GL46VertexBuffer)vertexBuffer,
            VertexStride = vertexBuffer.VertexStride
        };

        _commandList.Add(command);
    }

    public void SetVertexShader(IShader vertexShader)
    {
        // unused, pipeline will bind the program
    }

    public void SetViewport(Viewport viewport)
    {
        var command = new OpenGlCommand
        {
            Type = CommandType.SetViewport,
            Viewport = GLExtensions.Viewport(viewport)
        };

        _commandList.Add(command);
    }

    public void Submit()
    {
        foreach (var command in _commandList)
        {
            switch (command.Type)
            {
                case CommandType.Begin:
                    GL.Enable(EnableCap.DepthTest);
                    GL.DepthMask(true);
                    GL.DepthFunc(DepthFunction.Lequal);
                    GL.DepthRange(0f, 1f);

                    GL.Enable(EnableCap.Blend);
                    GL.Enable(EnableCap.LineSmooth);
                    GL.Enable(EnableCap.Multisample);
                    GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
                    break;
                case CommandType.End:
                    GL.BindVertexArray(0);
                    break;
                case CommandType.Draw:
                    GL.DrawArrays(_currentPrimitiveType, 0, command.DrawVertexCount);
                    break;
                case CommandType.DrawIndexed:
                    GL.DrawElements(_currentPrimitiveType, command.DrawIndexCount, DrawElementsType.UnsignedByte,
                        command.DrawVertexCount);
                    break;
                case CommandType.SetViewport:
                    GL.Viewport(command.Viewport);
                    break;
                case CommandType.SetScissor:
                    GL.Scissor(command.ScissorRectangle.X, command.ScissorRectangle.Y, command.ScissorRectangle.Width,
                        command.ScissorRectangle.Height);
                    break;
                case CommandType.SetPrimitiveTopology:
                    _currentPrimitiveType = command.PrimitiveType;
                    break;
                case CommandType.SetInputLayout:
                    GL.BindVertexArray(command.InputLayout);
                    _currentInputLayout = command.InputLayout;
                    break;
                case CommandType.SetDepthStencilState:
                    break;
                case CommandType.SetRasterizerState:
                    break;
                case CommandType.SetBlendState:
                    break;
                case CommandType.SetVertexBuffer:
                    GL.VertexArrayVertexBuffer(_currentInputLayout, 0, command.VertexBuffer, IntPtr.Zero,
                        command.VertexStride);
                    break;
                case CommandType.SetIndexBuffer:
                    GL.VertexArrayElementBuffer(_currentInputLayout, command.IndexBuffer);
                    break;
                case CommandType.SetVertexShader:
                    break;
                case CommandType.SetPixelShader:
                    break;
                case CommandType.SetComputeShader:
                    break;
                case CommandType.SetConstantBuffers:
                    GL.BindBufferBase(BufferRangeTarget.UniformBuffer, 0, command.ConstantBuffer);
                    break;
                case CommandType.SetSamplers:
                    break;
                case CommandType.SetTextures:
                    GL.BindTextureUnit(0, command.TextureView);
                    break;
                case CommandType.SetRenderTarget:
                    break;
                case CommandType.SetRenderTargets:
                    break;
                case CommandType.ClearRenderTarget:
                    GL.ClearColor(command.ClearColor);
                    GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
                    break;
                case CommandType.ClearDepthStencil:
                    GL.ClearDepth(command.ClearDepth);
                    GL.ClearStencil(command.ClearStencil);
                    break;
                case CommandType.SetPipeline:
                    GL.BindProgramPipeline(command.Pipeline);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    public void ClearRenderTarget(TextureView renderTarget, Vector4 clearColor)
    {
        var command = new OpenGlCommand
        {
            Type = CommandType.ClearRenderTarget,
            ClearColor = new OpenTK.Mathematics.Color4(clearColor.X, clearColor.Y, clearColor.Z, clearColor.W)
        };

        _commandList.Add(command);
    }

    private void SetProgramPipeline(GL46Pipeline pipeline)
    {
        var command = new OpenGlCommand
        {
            Type = CommandType.SetPipeline,
            Pipeline = pipeline
        };

        _commandList.Add(command);
    }
}