// 
// SketchRenderer.cs
// 
// Copyright (C) 2022 Yaman Alhalabi

using System;
using System.Linq;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Editor.Tools.Sketching;
using Fluint.Layer.EntityComponentSystem;
using Fluint.Layer.Graphics.API;
using Fluint.Layer.Graphics.Common;
using Fluint.Layer.Graphics.Renderers;
using Fluint.Layer.Mathematics;

namespace Fluint.Graphics.Base.Renderers;

public class SketchRenderer : ISketchRenderer
{
    private readonly IGraphicsFactory _graphicsFactory;
    private readonly ISketchSystem _system;

    private ICommandList _commandList;
    private Shader _fragmentShader;

    private IConstantBuffer _lineSetupBuffer;

    private IPipeline _pipeline;
    private IVertexBuffer _vertexBuffer;
    private int _vertexCount;

    private Shader _vertexShader;

    public SketchRenderer(ModulePacket packet)
    {
        _graphicsFactory = packet.CreateScoped<IGraphicsFactory>();

        var world = packet.GetSingleton<IWorld>();
        _system = world.GetSystem<ISketchSystem, ISketch>();
    }

    public ModelViewProjection WorldView
    {
        get;
        set;
    }

    public Viewport Viewport
    {
        get;
        set;
    }


    public void Start()
    {
        _commandList = _graphicsFactory.CreateCommandList();
        _vertexShader = _graphicsFactory.CreateShaderFromFile(
            ShaderStage.Vertex,
            "./base/shaders/sketch_lines.vert",
            VertexType.Position,
            Enumerable.Empty<(string, string)>());

        _fragmentShader = _graphicsFactory.CreateShaderFromFile(
            ShaderStage.Pixel,
            "./base/shaders/sketch_lines.frag",
            VertexType.Position,
            Enumerable.Empty<(string, string)>());

        _pipeline = _graphicsFactory.CreatePipeline(
            _vertexShader,
            _fragmentShader,
            _vertexShader.InputLayout,
            null, null, null,
            Viewport,
            PrimitiveTopology.Lines);

        _lineSetupBuffer = _graphicsFactory.CreateConstantBuffer(WorldView);
        _vertexBuffer = _graphicsFactory.CreateVertexBuffer(Array.Empty<PositionColorVertex>());
    }

    public void PreRender()
    {
        var lineVertices = _system.GetVertices();

        _lineSetupBuffer.UpdateBuffer(WorldView);

        _vertexCount = lineVertices.Length;
        _vertexBuffer.Initialize(lineVertices);

        _pipeline.Viewport = Viewport;
        GenerateCommandList();
    }

    public void Render()
    {
        _commandList.Submit();
    }

    public void PostRender()
    {
    }

    public void Dispose()
    {
    }

    private void GenerateCommandList()
    {
        _commandList.Clear();
        _commandList.Begin("Sketches Lines", _pipeline);
        _commandList.SetConstantBuffer(_lineSetupBuffer, BufferScope.VertexShader);
        _commandList.SetVertexBuffer(_vertexBuffer);
        _commandList.Draw(_vertexCount);
        _commandList.End();
    }
}