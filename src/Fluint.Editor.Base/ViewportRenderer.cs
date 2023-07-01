// 
// ViewportRenderer.cs
// 
// Copyright (C) 2022 Yaman Alhalabi

using System;
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
    private readonly IDebugRenderer _debugRenderer;
    private readonly IGridRenderer _gridRenderer;

    private readonly ModulePacket _packet;
    // private readonly ISketchRenderer _sketchRenderer;

    public ViewportRenderer(ModulePacket packet)
    {
        _packet = packet;
        _debug = packet.GetSingleton<IDebugRenderServer>();
        Camera = packet.CreateScoped<ICamera>();

        Pipeline = packet.CreateScoped<IRenderingPipeline>();

        _gridRenderer = packet.CreateScoped<IGridRenderer>();
        // _sketchRenderer = packet.CreateScoped<ISketchRenderer>();
        _debugRenderer = packet.CreateScoped<IDebugRenderer>();

        _commandList = packet.CreateScoped<IGraphicsFactory>().CreateCommandList();

        Pipeline.Renderers.Add(_gridRenderer);
        // Pipeline.Renderers.Add(_sketchRenderer);
        Pipeline.Renderers.Add(_debugRenderer);
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
        _debugRenderer.WorldView =
            _gridRenderer.WorldView =
                new ModelViewProjection(Matrix.Identity, Camera.GetViewMatrix(), Camera.GetProjectionMatrix());

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
        _gridRenderer.Viewport =
            _debugRenderer.Viewport = viewport;
    }

    public IViewportContext GetContext()
    {
        throw new NotImplementedException();
    }
}