// 
// RayTracingRenderer.cs
// 
// Copyright (C) 2022 Yaman Alhalabi

using Fluint.Layer.Graphics.API;
using Fluint.Layer.Graphics.Common;
using Fluint.Layer.Graphics.Renderers;
using Fluint.Layer.Mathematics;

namespace Fluint.Graphics.RayTracer;

public class RayTracingRenderer : IRenderer
{
    private static readonly Vector3[] Vertices = {
        new(-1.0f, -1.0f, -1.0f),
        new(-1.0f, 1.0f, -1.0f),
        new(1.0f, 1.0f, -1.0f),
        new(-1.0f, -1.0f, -1.0f),
        new(1.0f, 1.0f, -1.0f),
        new(1.0f, -1.0f, -1.0f)
    };

    private readonly IGraphicsFactory _graphicsFactory;

    private ICommandList _commandList;

    private Shader _fragmentShader;
    private IPipeline _pipeline;
    private IConstantBuffer _timeBuffer;
    private IVertexBuffer _vertexBuffer;
    private Shader _vertexShader;


    public RayTracingRenderer(IGraphicsFactory graphicsFactory)
    {
        _graphicsFactory = graphicsFactory;
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
            "./base/shaders/raytracer.vert",
            VertexType.Position,
            Enumerable.Empty<(string, string)>());

        _fragmentShader = _graphicsFactory.CreateShaderFromFile(
            ShaderStage.Pixel,
            "./base/shaders/raytracer.frag",
            VertexType.Position,
            Enumerable.Empty<(string, string)>());

        _pipeline = _graphicsFactory.CreatePipeline(
            _vertexShader,
            _fragmentShader,
            _vertexShader.InputLayout,
            null, null, null,
            new Viewport(0, 0, 500, 500),
            PrimitiveTopology.TriangleList);

        _timeBuffer = _graphicsFactory.CreateConstantBuffer(Environment.TickCount);
        GenerateCommandList();
    }

    public void PreRender()
    {
        GenerateCommandList();
    }

    public void Render()
    {
        _timeBuffer.UpdateBuffer(Environment.TickCount);
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
        _vertexBuffer = _graphicsFactory.CreateVertexBuffer(Vertices);

        _commandList.Clear();
        _commandList.Begin("RayTracer", _pipeline);
        // _commandList.SetConstantBuffer(_worldViewBuffer, BufferScope.VertexShader);
        _commandList.SetVertexBuffer(_vertexBuffer);
        _commandList.Draw(12);
        _commandList.End();
    }
}