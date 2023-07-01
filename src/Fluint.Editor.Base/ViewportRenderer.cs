// 
// ViewportRenderer.cs
// 
// Copyright (C) 2022 Yaman Alhalabi

using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Editor;
using Fluint.Layer.Graphics;
using Fluint.Layer.Graphics.API;
using Fluint.Layer.Graphics.Common;
using Fluint.Layer.Graphics.Renderers;
using Fluint.Layer.Mathematics;

namespace Fluint.Editor.Base;

public class ViewportRenderer : IViewportRenderer
{
    private readonly ICommandList _commandList;
    private readonly IGridRenderer _gridRenderer;
    private readonly ModulePacket _packet;
    private readonly ISketchRenderer _sketchRenderer;

    public ViewportRenderer(ModulePacket packet)
    {
        _packet = packet;
        Camera = packet.CreateScoped<ICamera>();

        Pipeline = packet.CreateScoped<IRenderingPipeline>();

        _gridRenderer = packet.CreateScoped<IGridRenderer>();
        _sketchRenderer = packet.CreateScoped<ISketchRenderer>();
        _commandList = packet.CreateScoped<IGraphicsFactory>().CreateCommandList();

        Pipeline.Renderers.Add(_gridRenderer);
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

    public void Resize(Viewport viewport)
    {
        _gridRenderer.Viewport = viewport;
    }
}