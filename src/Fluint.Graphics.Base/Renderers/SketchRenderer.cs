// 
// SketchRenderer.cs
// 
// Copyright (C) 2022 Yaman Alhalabi

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using Fluint.Layer.Graphics.API;
using Fluint.Layer.Graphics.Renderers;
using Fluint.Layer.Mathematics;

namespace Fluint.Graphics.Base.Renderers;

public class SketchRenderer : ISketchRenderer
{
    private readonly ICommandList _cmdList;

    private readonly IGraphicsFactory _factory;
    private readonly IPipeline _pipeline;
    private readonly List<ISketch> _sketches = new();

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

        _cmdList = _factory.CreateCommandList();
    }

    public IList<ISketch> Sketches => _sketches;

    public void Start()
    {
        _vertexCount = 0;
        _vertexBuffer = _factory.CreateVertexBuffer(Array.Empty<PositionColorVertex>());
    }

    public void PreRender()
    {
        throw new NotImplementedException();
    }

    public void Render()
    {
        throw new NotImplementedException();
    }

    public void PostRender()
    {
        throw new NotImplementedException();
    }

    public ISketch this[int index]
    {
        get => _sketches[index];
        set => _sketches[index] = value;
    }

    public IEnumerator<ISketch> GetEnumerator()
    {
        return _sketches.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)_sketches).GetEnumerator();
    }

    public void Add(ISketch item)
    {
        _sketches.Add(item);
    }

    public void Clear()
    {
        _sketches.Clear();
    }

    public bool Contains(ISketch item)
    {
        return _sketches.Contains(item);
    }

    public void CopyTo(ISketch[] array, int arrayIndex)
    {
        _sketches.CopyTo(array, arrayIndex);
    }

    public bool Remove(ISketch item)
    {
        return _sketches.Remove(item);
    }

    public int Count => _sketches.Count;

    public bool IsReadOnly => ((ICollection<ISketch>)_sketches).IsReadOnly;

    public void Dispose()
    {
    }
}