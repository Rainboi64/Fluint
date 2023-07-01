// 
// SketchRenderer.cs
// 
// Copyright (C) 2022 Yaman Alhalabi

using System;
using System.Collections.Immutable;
using Fluint.Layer.Editor.Tools.Sketching;
using Fluint.Layer.Graphics.API;
using Fluint.Layer.Graphics.Common;
using Fluint.Layer.Graphics.Renderers;
using Fluint.Layer.Mathematics;

namespace Fluint.Graphics.Base.Renderers;

public class SketchRenderer : ISketchRenderer
{
    private readonly ICommandList _cmdList;

    private readonly IGraphicsFactory _factory;
    private readonly IPipeline _pipeline;
    private readonly IConstantBuffer _worldViewBuffer;

    private ISketchSystem _system;
    private IVertexBuffer _vertexBuffer;
    private int _vertexCount;

    public SketchRenderer(IGraphicsFactory factory)
    {
        _factory = factory;

        var vertexShader = _factory.CreateShaderFromFile(
            ShaderStage.Vertex,
            "./base/shaders/simple.vert",
            VertexType.PositionColor,
            ImmutableArray<(string, string)>.Empty);

        var fragmentShader = _factory.CreateShaderFromFile(
            ShaderStage.Pixel,
            "./base/shaders/simple.frag",
            VertexType.PositionColor,
            ImmutableArray<(string, string)>.Empty);

        _pipeline = _factory.CreatePipeline(
            vertexShader,
            fragmentShader,
            vertexShader.InputLayout,
            null, null, null,
            new Viewport(0, 0, 750, 750),
            PrimitiveTopology.Lines);

        _worldViewBuffer = factory.CreateConstantBuffer(WorldView);
        _cmdList = _factory.CreateCommandList();
    }

    public void Start()
    {
        _vertexCount = 0;
        _vertexBuffer = _factory.CreateVertexBuffer(Array.Empty<PositionColorVertex>());
    }

    public void PreRender()
    {
        var vertex = _system.GetVertex();
        _vertexCount = vertex.Length;
        _vertexBuffer.Initialize(vertex);
        _worldViewBuffer.UpdateBuffer(WorldView);

        _cmdList.Clear();
        _cmdList.Begin("Grid", _pipeline);
        _cmdList.SetConstantBuffer(_worldViewBuffer, BufferScope.VertexShader);
        _cmdList.SetVertexBuffer(_vertexBuffer);
        _cmdList.Draw(_vertexCount);
        _cmdList.End();
    }

    public void Render()
    {
        _cmdList.Submit();
    }

    public void PostRender()
    {
    }

    public ModelViewProjection WorldView
    {
        get;
        set;
    }

    public void AttachSystem(ISketchSystem system)
    {
        _system = system;
    }

    public void Dispose()
    {
        _pipeline?.Dispose();
        _vertexBuffer?.Dispose();
    }
}