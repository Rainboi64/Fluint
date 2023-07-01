// 
// CameraControl.cs
// 
// Copyright (C) 2022 Yaman Alhalabi

using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Editor;
using Fluint.Layer.Graphics;
using Fluint.Layer.Graphics.API;
using Fluint.Layer.Input;
using Fluint.Layer.Localization;
using Fluint.Layer.Mathematics;
using Fluint.Layer.UI;
using Fluint.Layer.Windowing;

namespace Fluint.Editor.Layout.Base.Controls;

public class CameraControl : Control
{
    private readonly IBindingsManager _bindingsManager;
    private readonly IContainer _container;
    private readonly IMouseCapture _mouseCapture;
    private readonly ModulePacket _packet;
    private readonly IViewport _viewport;
    private readonly IViewportRenderer _viewportRenderer;
    private ICamera _camera;

    private IInputManager _inputManager;

    private IWindow _window;

    public CameraControl(ModulePacket packet)
    {
        _packet = packet;

        var localizationManager = packet.GetSingleton<ILocalizationManager>();
        var graphicsFactory = packet.CreateScoped<IGraphicsFactory>();

        _mouseCapture = packet.CreateScoped<IMouseCapture>();

        var swapChain =
            graphicsFactory.CreateSwapchain(new SwapChainDescriptor(750, 750, false, false, SwapEffect.Discard));


        _viewport = packet.CreateScoped<IViewport>();
        _viewport.SwapChain = swapChain;

        _bindingsManager = packet.CreateScoped<IBindingsManager>();

        _viewportRenderer = packet.CreateScoped<IViewportRenderer>();
        _viewportRenderer.SwapChain = swapChain;

        _container = packet.CreateScoped<IContainer>();
        _container.Title = localizationManager.Fetch("camera");
        _container.ScrollBar = false;

        _container.Add("Viewport", _viewport);
        Children.Add(_container);
    }


    public override void Begin(string name, IWindow window)
    {
        _window = window;
        _inputManager = window.InputManager;
        _bindingsManager.InputManager = _inputManager;

        _mouseCapture.Begin(window);

        _viewportRenderer.Start();
        _camera = _viewportRenderer.Camera;

        _camera.Fov = 90;

        _camera.Viewport = new Viewport(0, 0, 500, 500, 0.001f, 1000);
        _camera.ProjectionMode = ProjectionMode.Orthogonal;
        _camera.Position = new Vector3(0, 0, 0);
        _camera.Zoom = 0.01f;
        _camera.Pitch = 90;

        base.Begin(name, window);
    }

    public override void Tick()
    {
        _mouseCapture.Update();

        Input();

        _viewportRenderer.Render();


        if (_viewport.Size != _container.Size)
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

        var viewport = new Viewport(0, 0, _container.Size.X, _container.Size.Y, 0.01f, 100000f);

        _viewport.Size = _container.Size;
        _viewportRenderer.Resize(viewport);
    }

    private void Input()
    {
        if (!_container.IsFocused)
        {
            return;
        }

        const float camBoost = 0.1f;
        if (_bindingsManager.Get("MOVE_CAMERA") == InputState.Repeat)
        {
            if (_inputManager.IsMouseButtonPressed(MouseButton.Button1))
            {
                _mouseCapture.Capture();
                _camera.Pitch = _mouseCapture.Y * camBoost;
                _camera.Yaw = _mouseCapture.X * camBoost;
            }
        }

        _camera.Zoom -= _inputManager.MouseScrollDelta.Y / 100f;

        if (_inputManager.IsKeyPressed(Key.K))
        {
            _camera.ProjectionMode = _camera.ProjectionMode == ProjectionMode.Orthogonal
                ? ProjectionMode.Prespective
                : ProjectionMode.Orthogonal;
        }

        var boost = _inputManager.IsKeyPressed(Key.LeftShift) ? 20f : 10f;
        var movementVec = Vector3.Zero;

        if (_inputManager.IsKeyPressed(Key.W))
        {
            if (_camera.ProjectionMode == ProjectionMode.Orthogonal)
            {
                movementVec -= _camera.Up;
            }
            else
            {
                movementVec += _camera.Front;
            }
        }

        if (_inputManager.IsKeyPressed(Key.S))
        {
            if (_camera.ProjectionMode == ProjectionMode.Orthogonal)
            {
                movementVec += _camera.Up;
            }
            else
            {
                movementVec += _camera.Front;
            }
        }

        if (_inputManager.IsKeyPressed(Key.A))
        {
            movementVec -= _camera.Right;
        }

        if (_inputManager.IsKeyPressed(Key.D))
        {
            movementVec += _camera.Right;
        }

        _camera.Position += Vector3.Normalize(movementVec) * boost * (float)_window.FrameTime;
    }
}