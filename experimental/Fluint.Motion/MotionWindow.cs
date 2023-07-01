// 
// MotionWindow.cs
// 
// Copyright (C) 2022 Yaman Alhalabi

using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Graphics;
using Fluint.Layer.Graphics.Renderers;
using Fluint.Layer.Mathematics;
using Fluint.Layer.UI;
using Fluint.Layer.Windowing;

namespace Fluint.Motion;

public class MotionWindow : Control
{
    private readonly ICanvas _canvas;
    private readonly ICanvasRenderer _canvasRenderer;

    private readonly List<Vector2i> _vec2s = new();
    private IWindow _parent;

    private double _time;

    public MotionWindow(ModulePacket packet)
    {
        _canvas = packet.CreateScoped<ICanvas>();
        _canvas.InitializeCanvas(1024, 1024);
        var textureView = packet.CreateScoped<ITextureView>();
        textureView.Size = new Vector2i(1024, 1024);

        _canvasRenderer = packet.CreateScoped<ICanvasRenderer>();
        _canvasRenderer.Canvas = _canvas;

        var texture = _canvasRenderer.Texture;
        textureView.Texture = texture;

        Children.Add(textureView);
    }

    public override void Begin(string name, IWindow parent)
    {
        _parent = parent;
        base.Begin(name, parent);
    }

    public override void Tick()
    {
        _time += _parent.FrameTime;

        _canvas.Clear();
        _vec2s.Add(new Vector2i((int)(64 * _time), 512 + (int)(256 * Math.Sin(_time))));
        _canvas.DrawShape(Color.Aqua, _vec2s.ToArray());

        _canvasRenderer.Update();
        base.Tick();
    }
}