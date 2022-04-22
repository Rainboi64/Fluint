//
// GLWindowProvider.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;
using System.Collections.Generic;
using Fluint.Layer.Configuration;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Diagnostics;
using Fluint.Layer.Mathematics;
using Fluint.Layer.Tasks;
using Fluint.Layer.UI;
using Fluint.Layer.Windowing;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using Vector2i = OpenTK.Mathematics.Vector2i;

namespace Fluint.Engine.GL46.Windowing;

public class GlWindowProvider : GameWindow, IWindowProvider
{
    private readonly IConfigurationManager _configurationManager;
    private readonly ILogger _logger;

    private readonly ModulePacket _packet;
    private readonly ITaskManager _taskManager;

    public GlWindowProvider(ModulePacket packet, ILogger logger, ITaskManager taskManager,
        IConfigurationManager configurationManager) :
        base(GameWindowSettings.Default, new NativeWindowSettings {
            Size = new Vector2i(1600, 900), APIVersion = new Version(4, 5)
        })
    {
        _packet = packet;
        _taskManager = taskManager;
        _logger = logger;
        _configurationManager = configurationManager;

        VSync = VSyncMode.On;
        FrameQueue = new Queue<Action>();
    }

    public IWindow Client
    {
        get;
        private set;
    }

    public string WindowTitle
    {
        get => Title;
        set => FrameQueue.Enqueue(() => Title = value);
    }

    public bool WindowVSync
    {
        get => VSync == VSyncMode.On;
        set => FrameQueue.Enqueue(() => VSync = (VSyncMode)Convert.ToInt32(value));
    }

    public Layer.Mathematics.Vector2i WindowSize
    {
        get => OpenTkHelper.Vector2I(Size);
        set => Size = OpenTkHelper.Vector2I(value);
    }

    public Layer.Mathematics.Vector2i WindowLocation
    {
        get => OpenTkHelper.Vector2I(Location);
        set => Location = OpenTkHelper.Vector2I(value);
    }

    public Queue<Action> FrameQueue
    {
        get;
    }

    public object NativeKeyboardObject => KeyboardState;
    public object NativeMouseObject => MouseState;

    public void Start()
    {
        Client.OnStart();
        Run();
    }

    public void Adopt<TClient>() where TClient : IWindow
    {
        var instanceManager = _packet.GetSingleton<IGuiInstanceManager>();

        Client = (IWindow)_packet.FetchAndCreateInstance(typeof(TClient));
        Client.SetProvider(this);

        instanceManager.Adopt(Client);
        _logger.Information("[{1}] Adopted Client {0}", "OpenGL46", Client.Title);
    }

    protected override void OnLoad()
    {
        var config = _configurationManager.Get<WindowConfiguration>().VSync;
        WindowVSync = config;

        _logger.Debug("[{0}] Loaded", "OpenTK Window");

        _taskManager.Invoke(TaskSchedule.WindowReady, new TaskArgs(Client));

        Client.OnLoad();

        base.OnLoad();
    }

    protected override void OnUpdateFrame(FrameEventArgs args)
    {
        _taskManager.Invoke(TaskSchedule.WindowUpdate, new TaskArgs(Client));
        while (FrameQueue.Count > 0)
        {
            FrameQueue.Dequeue().Invoke();
        }

        Client.OnUpdate(args.Time);

        base.OnUpdateFrame(args);
    }

    protected override void OnRenderFrame(FrameEventArgs args)
    {
        GL.ClearColor(OpenTkHelper.Color4(new Color(24, 24, 24, 255)));
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit | ClearBufferMask.StencilBufferBit);

        _taskManager.Invoke(TaskSchedule.WindowRender, new TaskArgs(Client));
        Client.OnRender(args.Time);

        SwapBuffers();

        base.OnRenderFrame(args);
    }

    protected override void OnMouseWheel(MouseWheelEventArgs e)
    {
        _taskManager.Invoke(TaskSchedule.WindowMouseScroll, new TaskArgs(e.OffsetY));
        Client.OnMouseWheelMoved(OpenTkHelper.Vector2(e.Offset));

        _logger.Debug("[{0}] Scrolled Delta: {1}", "OpenTK Window", e.Offset);

        base.OnMouseWheel(e);
    }

    protected override void OnResize(ResizeEventArgs e)
    {
        GL.Viewport(0, 0, e.Width, e.Height);

        _logger.Debug("[{0}] Resized to ({1}, {2})", "OpenTK Window", e.Width, e.Height);

        _taskManager.Invoke(TaskSchedule.WindowResize, new TaskArgs(Client));
        Client.OnResize(e.Width, e.Height);

        base.OnResize(e);
    }

    protected override void OnTextInput(TextInputEventArgs e)
    {
        _taskManager.Invoke(TaskSchedule.WindowEnterText, new TaskArgs(new object[] { e.Unicode }, Client));
        Client.OnTextReceived(e.Unicode, e.AsString);

        _logger.Debug("[{0}] Text Input: {1}", "OpenTK Window", e.AsString);

        base.OnTextInput(e);
    }
}