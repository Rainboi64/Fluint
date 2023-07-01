// 
// DebugRenderer.cs
// 
// Copyright (C) 2022 Yaman Alhalabi

using System;
using System.Linq;
using Fluint.Layer.Graphics.API;
using Fluint.Layer.Graphics.Common;
using Fluint.Layer.Graphics.Renderers;
using Fluint.Layer.Mathematics;

namespace Fluint.Graphics.Base.Renderers;

public class DebugRenderer : IDebugRenderer
{
    private readonly IGraphicsFactory _graphicsFactory;
    private readonly IDebugRenderServer _renderServer;

    private ICommandList _commandList;
    private Shader _fragmentShader;

    private IPipeline _pipeline;
    private IVertexBuffer _vertexBuffer;
    private int _vertexCount;

    private Shader _vertexShader;
    private Viewport _viewport;
    private bool _viewportUpdated;
    private IConstantBuffer _worldViewBuffer;

    public DebugRenderer(IDebugRenderServer renderServer, IGraphicsFactory graphicsFactory)
    {
        _renderServer = renderServer;
        _renderServer.OnVertexChanged += RenderServerOnOnVertexChanged;
        _graphicsFactory = graphicsFactory;
    }

    public ModelViewProjection WorldView
    {
        get;
        set;
    }

    public Viewport Viewport
    {
        get => _viewport;
        set
        {
            _viewportUpdated = true;
            _viewport = value;
        }
    }


    public void Start()
    {
        _commandList = _graphicsFactory.CreateCommandList();
        _vertexShader = _graphicsFactory.CreateShaderFromFile(
            ShaderStage.Vertex,
            "./base/shaders/grid.vert",
            VertexType.PositionColor,
            Enumerable.Empty<(string, string)>());

        _fragmentShader = _graphicsFactory.CreateShaderFromFile(
            ShaderStage.Pixel,
            "./base/shaders/grid.frag",
            VertexType.PositionColor,
            Enumerable.Empty<(string, string)>());

        _pipeline = _graphicsFactory.CreatePipeline(
            _vertexShader,
            _fragmentShader,
            _vertexShader.InputLayout,
            null, null, null,
            Viewport,
            PrimitiveTopology.Lines);

        _worldViewBuffer = _graphicsFactory.CreateConstantBuffer(WorldView);
        _vertexBuffer = _graphicsFactory.CreateVertexBuffer(Array.Empty<PositionColorVertex>());
    }

    public void PreRender()
    {
        if (!_viewportUpdated)
        {
            return;
        }

        _pipeline.Viewport = _viewport;
        GenerateCommandList();

        _viewportUpdated = false;
    }

    public void Render()
    {
        _pipeline.Viewport = Viewport;
        _worldViewBuffer.UpdateBuffer(WorldView);
        _commandList.Submit();
    }

    public void PostRender()
    {
    }

    public void Dispose()
    {
    }

    private void RenderServerOnOnVertexChanged(object sender, EventArgs e)
    {
        _vertexCount = _renderServer.DebugVertex.Length;
        _vertexBuffer.Initialize(_renderServer.DebugVertex);

        GenerateCommandList();
    }

    private void GenerateCommandList()
    {
        _commandList.Clear();
        _commandList.Begin("Debug", _pipeline);
        _commandList.SetConstantBuffer(_worldViewBuffer, BufferScope.VertexShader);
        _commandList.SetVertexBuffer(_vertexBuffer);
        _commandList.Draw(_vertexCount);
        _commandList.End();
    }
}