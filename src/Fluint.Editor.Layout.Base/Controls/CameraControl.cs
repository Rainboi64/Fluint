// 
// CameraControl.cs
// 
// Copyright (C) 2022 Yaman Alhalabi

using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Editor.Viewport;
using Fluint.Layer.Graphics.API;
using Fluint.Layer.Localization;
using Fluint.Layer.Mathematics;
using Fluint.Layer.UI;
using Fluint.Layer.Windowing;

namespace Fluint.Editor.Layout.Base.Controls;

public class CameraControl : Control
{
    private readonly IContainer _container;
    private readonly IViewport _viewport;
    private readonly IViewportContext _viewportContext;

    private IWindow _window;

    public CameraControl(ModulePacket packet)
    {
        _viewportContext = packet.CreateScoped<IViewportContext>();

        var localizationManager = packet.GetSingleton<ILocalizationManager>();
        var graphicsFactory = packet.CreateScoped<IGraphicsFactory>();

        var swapChain =
            graphicsFactory.CreateSwapchain(new SwapChainDescriptor(750, 750, false, false, SwapEffect.Discard));

        _viewport = packet.CreateScoped<IViewport>();
        _viewport.SwapChain = swapChain;

        var viewportRenderer = packet.CreateScoped<IViewportRenderer>();
        viewportRenderer.SwapChain = swapChain;
        _viewportContext.AttachRenderer(viewportRenderer);

        _container = packet.CreateScoped<IContainer>();
        _container.Title = localizationManager.Fetch("camera");
        _container.ScrollBar = false;

        _container.Add("Viewport", _viewport);
        Children.Add(_container);
    }


    public override void Begin(string name, IWindow window)
    {
        _window = window;
        _viewportContext.Window = window;
        _viewportContext.Start();
        base.Begin(name, window);
    }

    public override void Tick()
    {
        _viewportContext.Focused = _container.IsFocused;
        _viewportContext.Update();

        if (_viewport.Size != _container.Size || _container.Location != _viewportContext.Location)
        {
            Resize();
        }

        base.Tick();
    }

    private void Resize()
    {
        if (_container.Size.X <= 0 || _container.Size.Y <= 0)
        {
            return;
        }

        _viewportContext.Location = _container.Location;
        var viewport = new Viewport(0, 0, _container.Size.X, _container.Size.Y, 0.01f, 100000f);

        _viewport.Size = _container.Size;
        _viewportContext.Resize(viewport);
    }
}