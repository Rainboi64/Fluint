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
    private readonly IDebugRenderServer _debug;

    private readonly ModulePacket _packet;

    public ViewportRenderer(ModulePacket packet)
    {
        _packet = packet;
        _debug = packet.GetSingleton<IDebugRenderServer>();
        Camera = packet.CreateScoped<ICamera>();

        Pipeline = packet.CreateScoped<IRenderingPipeline>();
        _commandList = packet.CreateScoped<IGraphicsFactory>().CreateCommandList();

        var gridRenderer = packet.CreateScoped<IGridRenderer>();
        var sketchRenderer = packet.CreateScoped<ISketchRenderer>();
        var debugRenderer = packet.CreateScoped<IDebugRenderer>();

        Pipeline.Renderers.Add(gridRenderer);
        Pipeline.Renderers.Add(sketchRenderer);
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