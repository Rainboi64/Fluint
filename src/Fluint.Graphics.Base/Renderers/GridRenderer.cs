// 
// GridRenderer.cs
// 
// Copyright (C) 2022 Yaman Alhalabi

using System;
using System.Linq;
using Fluint.Layer.Editor.Viewport;
using Fluint.Layer.Graphics.API;
using Fluint.Layer.Graphics.Common;
using Fluint.Layer.Graphics.Renderers;
using Fluint.Layer.Mathematics;

namespace Fluint.Graphics.Base.Renderers;

public class GridRenderer : IGridRenderer
{
    private readonly IGraphicsFactory _graphicsFactory;

    private ICommandList _commandList;
    private Shader _fragmentShader;
    private Grid _grid;

    private IPipeline _pipeline;
    private IVertexBuffer _vertexBuffer;

    private int _vertexCount;
    private Shader _vertexShader;
    private Viewport _viewport;
    private bool _viewportUpdated;
    private IConstantBuffer _worldViewBuffer;

    public GridRenderer(IGraphicsFactory graphicsFactory)
    {
        _graphicsFactory = graphicsFactory;
        _viewportUpdated = true;
    }

    public Grid Grid
    {
        get => _grid;
        set => _grid = value;
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
        if (_grid.NeedsUpdate)
        {
            GenerateGrid();
            GenerateCommandList();

            _grid.NeedsUpdate = false;
        }

        if (_viewportUpdated)
        {
            _pipeline.Viewport = _viewport;
            GenerateCommandList();

            _viewportUpdated = false;
        }
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


    private void GenerateGrid()
    {
        var gridLines =
            new PositionColorVertex[(_grid.Size.X + _grid.Size.Y) * 4 + 4];
        var k = 0;

        for (var x = -_grid.Size.X; x <= _grid.Size.X; x += _grid.Offsets.X)
        {
            var color = new Vector4(0.2f);
            if (x % 10 == 0)
            {
                color = new Vector4(0.4f);
            }

            if (x == 0)
            {
                color = new Vector4(0.8f, 0.2f, 0.2f, 1);
            }

            gridLines[k++] = new PositionColorVertex(new Vector3(x, 0, -_grid.Size.Y), color);
            gridLines[k++] = new PositionColorVertex(new Vector3(x, 0, _grid.Size.Y), color);
        }

        for (var y = -_grid.Size.Y; y <= _grid.Size.Y; y += _grid.Offsets.Y)
        {
            var color = new Vector4(0.2f);
            if (y % 10 == 0)
            {
                color = new Vector4(0.4f);
            }

            if (y == 0)
            {
                color = new Vector4(0.2f, 0.2f, 0.8f, 1);
            }

            gridLines[k++] = new PositionColorVertex(new Vector3(-_grid.Size.X, 0, y), color);
            gridLines[k++] = new PositionColorVertex(new Vector3(_grid.Size.X, 0, y), color);
        }

        _vertexCount = k;
        _vertexBuffer.Initialize(gridLines);
    }

    private void GenerateCommandList()
    {
        _commandList.Clear();
        _commandList.Begin("Grid", _pipeline);
        _commandList.SetConstantBuffer(_worldViewBuffer, BufferScope.VertexShader);
        _commandList.SetVertexBuffer(_vertexBuffer);
        _commandList.Draw(_vertexCount);
        _commandList.End();
    }
}