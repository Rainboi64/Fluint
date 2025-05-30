
//
// ViewportRenderer.cs
//
// Copyright (C) 2022 Yaman Alhalabi

using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Editor.Viewport;
using Fluint.Layer.Graphics;
using Fluint.Layer.Graphics.API;
using Fluint.Layer.Graphics.Common;
using Fluint.Layer.Graphics.Renderers;
using Fluint.Layer.Mathematics;
using Fluint.Layer.Windowing;

namespace Fluint.Editor.Base;

public class ViewportRenderer : IViewportRenderer
{
    private readonly ICommandList _commandList;
    private readonly Layer.Mathematics.Viewport _viewport;

    public ViewportRenderer(ModulePacket packet)
    {
        Camera = packet.CreateScoped<ICamera>();

        Pipeline = packet.CreateScoped<IRenderingPipeline>();
        _commandList = packet.CreateScoped<IGraphicsFactory>().CreateCommandList();

        _viewport = new Layer.Mathematics.Viewport(0, 0, 250, 250);
        Grid = new Grid(new Vector2(0.5f, 0.5f), new Vector2i(64, 64));

        var gridRenderer = packet.CreateScoped<IGridRenderer>();
        gridRenderer.Viewport = _viewport;
        gridRenderer.Grid = Grid;

        var sketchRenderer = packet.CreateScoped<ISketchRenderer>();
        sketchRenderer.Viewport = _viewport;

        var debugRenderer = packet.CreateScoped<IDebugRenderer>();
        debugRenderer.Viewport = _viewport;

        var gizmoRenderer = packet.CreateScoped<IGizmoRenderer>();
        gizmoRenderer.Viewport = _viewport;

        Pipeline.Renderers.Add(gridRenderer);
        Pipeline.Renderers.Add(sketchRenderer);
        Pipeline.Renderers.Add(gizmoRenderer);
        Pipeline.Renderers.Add(debugRenderer);
    }

    public IWindow Window
    {
        get;
        set;
    }

    public ICamera Camera
    {
        get;
    }

    public Grid Grid
    {
        get;
    }

    public IRenderingPipeline Pipeline
    {
        get;
    }

    public ISwapChain SwapChain
    {
        get;
        set;
    }

    public void Start()
    {
        Pipeline.Start();
        _commandList.ClearRenderTarget(SwapChain.TextureView, Color.Transparent);
    }

    public void Render()
    {
        foreach (var renderer in Pipeline.Renderers)
        {
            renderer.WorldView =
                new ModelViewProjection(Matrix.Identity, Camera.GetViewMatrix(), Camera.GetProjectionMatrix());
        }

        SwapChain.Present();
        {
            _commandList.Submit();

            Pipeline.PreRender();
            Pipeline.Render();
            Pipeline.PostRender();
        }
        SwapChain.Disconnect();
    }

    public void Resize(Layer.Mathematics.Viewport viewport)
    {
        _viewport.X = viewport.X;
        _viewport.Y = viewport.Y;
        _viewport.Width = viewport.Width;
        _viewport.Height = viewport.Height;
        _viewport.MaxDepth = viewport.MaxDepth;
        _viewport.MinDepth = viewport.MinDepth;
    }
}
