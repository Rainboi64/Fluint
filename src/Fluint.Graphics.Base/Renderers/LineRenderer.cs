//
// LineRenderer.cs
//
// Copyright (C) 2023 Yaman Alhalabi

using System;
using System.Linq;
using System.Runtime.InteropServices;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Graphics.API;
using Fluint.Layer.Graphics.Common;
using Fluint.Layer.Graphics.Renderers;
using Fluint.Layer.Mathematics;

namespace Fluint.Graphics.Base.Renderers;
public abstract class LineRenderer : ILineRenderer
{
    protected const int MaxVertices = 1024;

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    protected struct LineShaderBufferObject
    {
        public ModelViewProjection WorldView;
        public Vector2 Resolution;
        public Vector2 AntiAliasRadius;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MaxVertices)]
        public PositionWidthColorVertex[] Vertex;

        public LineShaderBufferObject(ModelViewProjection worldView, Vector2 resolution, Vector2 antiAliasRadius, PositionWidthColorVertex[] vertex)
        {
            WorldView = worldView;
            Resolution = resolution;
            AntiAliasRadius = antiAliasRadius;
            Vertex = new PositionWidthColorVertex[MaxVertices + 4];
            Array.Copy(vertex, Vertex, vertex.Length);
        }
    }

    protected ICommandList CommandList;

    private readonly string _passName = "Lines";
    private readonly IGraphicsFactory _graphicsFactory;
    private IPipeline _pipeline;
    private IConstantBuffer _shaderBuffer;
    private Shader _fragmentShader;
    private Shader _vertexShader;

    protected LineShaderBufferObject BufferObject;


    public LineRenderer(IGraphicsFactory graphicsFactory)
    {
        _graphicsFactory = graphicsFactory;
        BufferObject = new LineShaderBufferObject(WorldView, Vector2.Zero, new Vector2(2, 2), new PositionWidthColorVertex[MaxVertices + 4]);
    }

    protected LineRenderer(IGraphicsFactory graphicsFactory, string passName)
    {
        _graphicsFactory = graphicsFactory;
        BufferObject = new LineShaderBufferObject(WorldView, Vector2.Zero, new Vector2(2, 2), new PositionWidthColorVertex[MaxVertices + 4]);
        _passName = passName;
    }

    public ModelViewProjection WorldView
    {
        get => BufferObject.WorldView;
        set => BufferObject.WorldView = value;
    }

    public Viewport Viewport
    {
        get;
        set;
    }

    public void Start()
    {
        CommandList = _graphicsFactory.CreateCommandList();
        _vertexShader = _graphicsFactory.CreateShaderFromFile(
            ShaderStage.Vertex,
            "./base/shaders/grid.vert",
            VertexType.Unknown,
            Enumerable.Empty<(string, string)>());

        _fragmentShader = _graphicsFactory.CreateShaderFromFile(
            ShaderStage.Pixel,
            "./base/shaders/grid.frag",
            VertexType.Unknown,
            Enumerable.Empty<(string, string)>());

        _pipeline = _graphicsFactory.CreatePipeline(
            _vertexShader,
            _fragmentShader,
            _vertexShader.InputLayout,
            null, null, null,
            Viewport,
            PrimitiveTopology.TriangleList);


        _shaderBuffer = _graphicsFactory.CreateConstantBuffer(BufferObject);

        CommandList.Clear();
        CommandList.Begin(_passName, _pipeline);
        CommandList.SetConstantBuffer(_shaderBuffer, BufferScope.VertexShader);
        CommandList.Draw(6*(MaxVertices-1));
        CommandList.End();
    }

    public void PreRender()
    {
        BufferObject.Resolution = new Vector2(Viewport.Width, Viewport.Height);
        _shaderBuffer.UpdateBuffer(BufferObject);
    }

    public void Render()
    {
        CommandList.Submit();
    }

    public void PostRender()
    {
    }

    public void Dispose()
    {
        _shaderBuffer.Dispose();
    }
}


#if USE_FA
public abstract class LineRenderer : ILineRenderer
{
    protected const int MaxVertices = 65536;

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    protected struct LineShaderBufferObject
    {
        public ModelViewProjection WorldView;
        public Vector2 Resolution;
        public Vector2 AntiAliasRadius;
        public PositionWidthColorVertex[] Vertex;

        public LineShaderBufferObject(ModelViewProjection worldView, Vector2 resolution, Vector2 antiAliasRadius, PositionWidthColorVertex[] vertex)
        {
            WorldView = worldView;
            Resolution = resolution;
            AntiAliasRadius = antiAliasRadius;
            Vertex = new PositionWidthColorVertex[MaxVertices + 4];
            Array.Copy(vertex, Vertex, vertex.Length);
        }
    }

    private readonly IGraphicsFactory _graphicsFactory;
    private readonly string _passName = "Lines";

    private ICommandList _commandList;
    private Shader _fragmentShader;

    private IConstantBuffer _lineSetupBuffer;

    private IPipeline _pipeline;
    private IVertexBuffer _vertexBuffer;
    private int _vertexCount;

    private Shader _vertexShader;
    protected LineShaderBufferObject BufferObject;


    public LineRenderer(ModulePacket packet)
    {
        _graphicsFactory = packet.CreateScoped<IGraphicsFactory>();
        BufferObject = new LineShaderBufferObject(WorldView, Vector2.Zero, new Vector2(2, 2), new PositionWidthColorVertex[MaxVertices + 4]);
    }

    protected LineRenderer(IGraphicsFactory graphicsFactory, string passName)
    {
        _graphicsFactory = graphicsFactory;
        _passName = passName;
        BufferObject = new LineShaderBufferObject(WorldView, Vector2.Zero, new Vector2(2, 2), new PositionWidthColorVertex[MaxVertices + 4]);

    }

    public ModelViewProjection WorldView
    {
        get => BufferObject.WorldView;
        set => BufferObject.WorldView = value;
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

        _lineSetupBuffer.UpdateBuffer(WorldView);

        _vertexCount = BufferObject.Vertex.Length;
        _vertexBuffer.Initialize(BufferObject.Vertex);

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
        _commandList.Begin(_passName, _pipeline);
        _commandList.SetConstantBuffer(_lineSetupBuffer, BufferScope.VertexShader);
        _commandList.SetVertexBuffer(_vertexBuffer);
        _commandList.Draw(_vertexCount);
        _commandList.End();
    }
}

#endif
