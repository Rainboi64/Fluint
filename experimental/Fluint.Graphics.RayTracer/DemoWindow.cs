// 
// DemoWindow.cs
// 
// Copyright (C) 2022 Yaman Alhalabi

using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Graphics.API;
using Fluint.Layer.Graphics.Renderers;
using Fluint.Layer.Mathematics;
using Fluint.Layer.UI;
using Fluint.Layer.Windowing;

namespace Fluint.Graphics.RayTracer;

public class DemoWindow : Control
{
    private readonly ICommandList _commandList;
    private readonly IRenderer _rayTracingRenderer;
    private readonly ISwapChain _swapChain;

    public DemoWindow(ModulePacket packet)
    {
        var graphicsFactory = packet.CreateScoped<IGraphicsFactory>();
        _commandList = graphicsFactory.CreateCommandList();

        _rayTracingRenderer = new RayTracingRenderer(graphicsFactory);

        _swapChain =
            graphicsFactory.CreateSwapchain(new SwapChainDescriptor(500, 500, false, false, SwapEffect.Discard));


        var viewport = packet.CreateScoped<IViewport>();
        viewport.SwapChain = _swapChain;

        var container = packet.CreateScoped<IContainer>();
        container.Title = "Ray Tracer";
        container.ScrollBar = false;

        container.Add("Viewport", viewport);
        Children.Add(container);
    }


    public override void Begin(string name, IWindow parent)
    {
        base.Begin(name, parent);
        _commandList.ClearRenderTarget(_swapChain.TextureView, Color.Transparent);
        _rayTracingRenderer.Start();
    }


    public override void Tick()
    {
        _swapChain.Present();

        _commandList.Submit();
        _rayTracingRenderer.PreRender();
        _rayTracingRenderer.Render();
        _rayTracingRenderer.PostRender();
        _swapChain.Disconnect();
        base.Tick();
    }
}