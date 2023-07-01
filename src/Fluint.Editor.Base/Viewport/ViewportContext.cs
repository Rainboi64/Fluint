// 
// ViewportContext.cs
// 
// Copyright (C) 2022 Yaman Alhalabi

using System;
using System.Collections.Generic;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Editor.Tools;
using Fluint.Layer.Editor.Viewport;
using Fluint.Layer.Graphics;
using Fluint.Layer.Graphics.API;
using Fluint.Layer.Input;
using Fluint.Layer.Mathematics;
using Fluint.Layer.StateManagement;
using Fluint.Layer.Windowing;

namespace Fluint.Editor.Base.Viewport;

public class ViewportContext : IViewportContext
{
    private readonly ModulePacket _packet;

    private readonly Dictionary<Type, IState> _toolState = new();
    private IViewportRenderer _viewportRenderer;

    public ViewportContext(ModulePacket packet)
    {
        _packet = packet;
        BindingsManager = packet.CreateScoped<IBindingsManager>();
        InputManager = packet.CreateScoped<IInputManager>();
    }

    public IWindow Window
    {
        get;
        set;
    }

    public ICamera Camera
    {
        get;
        private set;
    }

    public IInputManager InputManager
    {
        get;
        private set;
    }

    public IBindingsManager BindingsManager
    {
        get;
    }

    public Layer.Mathematics.Viewport Viewport
    {
        get;
        private set;
    }

    public Vector2i Location
    {
        get;
        set;
    }

    public bool Focused
    {
        get;
        set;
    }

    public void Start()
    {
        Camera = _viewportRenderer.Camera;

        Camera.Fov = 90;

        Camera.Viewport = new Layer.Mathematics.Viewport(0, 0, 500, 500, 0.001f, 1000);
        Camera.ProjectionMode = ProjectionMode.Orthogonal;
        Camera.Position = new Vector3(0, 0, 0);
        Camera.Zoom = 0.01f;
        Camera.Pitch = 90;

        _viewportRenderer.Start();

        InputManager = Window.InputManager;
        BindingsManager.InputManager = InputManager;

        var tools = _packet.GetInstances<ITool>();
        foreach (var tool in tools)
        {
            tool.Begin(this);
        }
    }

    public void Update()
    {
        OnTick?.Invoke(this, new TickEventArgs(Window.FrameTime));
        _viewportRenderer.Render();
    }

    public void Resize(Layer.Mathematics.Viewport viewport)
    {
        Viewport = viewport;
        _viewportRenderer.Resize(viewport);
    }

    public void AttachRenderer(IViewportRenderer renderer)
    {
        _viewportRenderer = renderer;
    }

    public event EventHandler<TickEventArgs> OnTick;

    public void AddState<T>(T state) where T : IState
    {
        _toolState.Add(typeof(T), state);
    }

    public T GetState<T>() where T : IState
    {
        return (T)_toolState[typeof(T)];
    }
}