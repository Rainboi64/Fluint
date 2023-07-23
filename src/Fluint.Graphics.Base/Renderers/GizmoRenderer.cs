// 
// GizmoRenderer.cs
// 
// Copyright (C) 2022 Yaman Alhalabi

using System;
using System.Collections.Generic;
using System.Linq;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Editor.Gizmos;
using Fluint.Layer.Graphics.API;
using Fluint.Layer.Graphics.Common;
using Fluint.Layer.Graphics.Renderers;
using Fluint.Layer.Mathematics;

namespace Fluint.Graphics.Base.Renderers;

public class GizmoRenderer : IGizmoRenderer
{
    private readonly IGraphicsFactory _graphicsFactory;
    private readonly IGizmoProvider _provider;

    private ICommandList _commandList;
    private Shader _fragmentShader;

    private IPipeline _pipeline;
    private IVertexBuffer _vertexBuffer;
    private int _vertexCount;

    private Shader _vertexShader;
    private IConstantBuffer _worldViewBuffer;

    public GizmoRenderer(ModulePacket packet)
    {
        _graphicsFactory = packet.CreateScoped<IGraphicsFactory>();
        _provider = packet.GetSingleton<IGizmoProvider>();
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
            PrimitiveTopology.TriangleList);

        _worldViewBuffer = _graphicsFactory.CreateConstantBuffer(WorldView);
        _vertexBuffer = _graphicsFactory.CreateVertexBuffer(Array.Empty<PositionColorVertex>());
    }

    public void PreRender()
    {
        var vertices = new List<PositionColorVertex>();
        foreach (var gizmo in _provider.Gizmos)
        {
            vertices.AddRange(gizmo.GenerateVertex());
        }

        _vertexCount = vertices.Count;
        _vertexBuffer.Initialize(vertices.ToArray());

        _pipeline.Viewport = Viewport;
        GenerateCommandList();
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

    private void GenerateCommandList()
    {
        _commandList.Clear();
        _commandList.Begin("Gizmos", _pipeline);
        _commandList.SetConstantBuffer(_worldViewBuffer, BufferScope.VertexShader);
        _commandList.SetVertexBuffer(_vertexBuffer);
        _commandList.Draw(_vertexCount);
        _commandList.End();
    }
}