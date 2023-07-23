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

    public ViewportRenderer(ModulePacket packet)
    {
        Camera = packet.CreateScoped<ICamera>();

        Pipeline = packet.CreateScoped<IRenderingPipeline>();
        _commandList = packet.CreateScoped<IGraphicsFactory>().CreateCommandList();

        Grid = new Grid(new Vector2i(1, 1), new Vector2i(1024, 1024));

        var gridRenderer = packet.CreateScoped<IGridRenderer>();
        gridRenderer.Grid = Grid;

        var sketchRenderer = packet.CreateScoped<ISketchRenderer>();
        var debugRenderer = packet.CreateScoped<IDebugRenderer>();
        var gizmoRenderer = packet.CreateScoped<IGizmoRenderer>();

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
        foreach (var renderer in Pipeline.Renderers)
        {
            renderer.Viewport = viewport;
        }
    }
}