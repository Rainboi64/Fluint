// 
// SketchRenderer.cs
// 
// Copyright (C) 2022 Yaman Alhalabi

using System;
using System.Threading.Tasks;
using Fluint.Layer.Diagnostics;
using Fluint.Layer.Editor.Tools.Sketching;
using Fluint.Layer.EntityComponentSystem;
using Fluint.Layer.Graphics.API;
using Fluint.Layer.Graphics.Renderers;
using Fluint.Layer.Mathematics;

namespace Fluint.Graphics.Base.Renderers;

public class SketchRenderer : LineRenderer, ISketchRenderer
{
    private readonly ISketchSystem _system;
    private readonly Vector4 _color = (Vector4)Color.Wheat;
    private readonly ILogger _logger;
    private const float LineWidth = 1;

    private bool _overflowed = false;

    public SketchRenderer(IWorld world, IGraphicsFactory factory, ILogger logger) : base(factory, "Sketch Lines")
    {
        _system = world.GetSystem<ISketchSystem, ISketch>();
        _logger = logger;
    }

    long map(long x, long in_min, long in_max, long out_min, long out_max)
    {
        return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
    }
    
    public new void PreRender()
    {
        if (_overflowed)
        {
            base.PreRender();
            return;
        }
        
        var lineVertices = _system.GetVertices();
        var vertLen = lineVertices.Length;
        
        var colors = _system.GetColors();
        var colorsLen = colors.Length;
        
        var len = Math.Min(lineVertices.Length, BufferObject.Vertex.Length);
        
        BufferObject.Vertex = new PositionWidthColorVertex[MaxVertices];

        var sketchesLen = _system.Sketches.Count;
        Parallel.For(0, len, i =>
        {
            BufferObject.Vertex[i] = new PositionWidthColorVertex(lineVertices[i], LineWidth, Color.White.ToVector4());
        });
        
        if (lineVertices.Length > BufferObject.Vertex.Length && !_overflowed)
        {
            _logger.Warning("[{0}] Uniform Buffer Exhaustion", "Sketch Renderer");
            _overflowed = true;
        }
        
        base.PreRender();
    }

    public FillMode FillMode
    {
        get;
        set;
    }
}