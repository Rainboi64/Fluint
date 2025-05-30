//
// GLWindowProvider.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;
using System.Collections.Concurrent;
using Fluint.Layer.Configuration;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Diagnostics;
using Fluint.Layer.Graphics.API;
using Fluint.Layer.Jobs;
using Fluint.Layer.Mathematics;
using Fluint.Layer.Miscellaneous;
using Fluint.Layer.UI;
using Fluint.Layer.Windowing;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using Vector2 = Fluint.Layer.Mathematics.Vector2;
using Vector2i = OpenTK.Mathematics.Vector2i;

namespace Fluint.Graphics.API.GLCommon.Windowing;

public class GlWindowProvider : GameWindow, IWindowProvider
{
    private readonly IConfigurationManager _configurationManager;
    private readonly IJobManager _jobManager;
    private readonly ILogger _logger;

    private readonly ModulePacket _packet;

    public GlWindowProvider(ModulePacket packet, ILogger logger, IJobManager jobManager,
        IConfigurationManager configurationManager) :
        base(new GameWindowSettings(), new NativeWindowSettings
        {
            Flags = ContextFlags.ForwardCompatible,
            APIVersion = new Version(configurationManager.Get<GlfwWindowConfiguration>().MajorOpenGLVersion, configurationManager.Get<GlfwWindowConfiguration>().MinorOpenGLVersion),
            Size = new Vector2i(800, 600),
            NumberOfSamples = 0
        })
    {

        _packet = packet;
        _jobManager = jobManager;
        _logger = logger;
        _configurationManager = configurationManager;

        VSync = VSyncMode.On;
        FrameQueue = new ConcurrentQueue<Action>();
    }

    public IWindow Client
    {
        get;
        private set;
    }

    public void SetMouseLocation(Vector2 location)
    {
        MousePosition = GLExtensions.Vector2(location);
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
        get => GLExtensions.Vector2I(Size);
        set => Size = GLExtensions.Vector2I(value);
    }

    public Layer.Mathematics.Vector2i WindowLocation
    {
        get => GLExtensions.Vector2I(Location);
        set => Location = GLExtensions.Vector2I(value);
    }

    public Layer.Mathematics.Vector2i ScreenSize
    {
        get
        {
            unsafe
            {
                var videoMode = GLFW.GetVideoMode(CurrentMonitor.ToUnsafePtr<Monitor>());
                return new Layer.Mathematics.Vector2i(videoMode->Width, videoMode->Height);
            }
        }
    }

    public Vector2 ScaleRate
    {
        get;
        private set;
    }

    public ConcurrentQueue<Action> FrameQueue
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

    public void Adopt(IWindow window)
    {
        var instanceManager = _packet.GetSingleton<IGuiInstanceManager>();

        Client = window;
        Client.SetProvider(this);

        instanceManager.Adopt(Client);
        _logger.Verbose("[{1}] Adopted Client {0}", "OpenTK Window", Client.Title);
    }

    protected override unsafe void OnLoad()
    {
        var config = _configurationManager.Get<WindowConfiguration>();

        GLFW.SetInputMode(WindowPtr, StickyAttributes.StickyMouseButtons, true);
        
        Size = GLExtensions.Vector2I(config.WindowSize);
        WindowBorder = config.Resizable ? WindowBorder.Resizable : WindowBorder.Fixed;

        RenderFrequency = config.FrameLimit;
        UpdateFrequency = config.FrameLimit;

        _logger.Debug("[{0}] Loaded", "OpenTK Window");

        _jobManager.Invoke(JobSchedule.WindowReady, new JobArgs(Client));

        Client.OnLoad();

        base.OnLoad();
    }

    protected override void OnUpdateFrame(FrameEventArgs args)
    {
        _jobManager.Invoke(JobSchedule.WindowUpdate, new JobArgs(Client));
        while (FrameQueue.Count > 0)
        {
            if (FrameQueue.TryDequeue(out var action))
            {
                action();
            }
        }

        Client.OnUpdate(args.Time);

        base.OnUpdateFrame(args);
    }

    protected override unsafe void OnRenderFrame(FrameEventArgs args)
    {
        GLFW.GetFramebufferSize(this.WindowPtr, out var width, out var height);

        ScaleRate = new Vector2((float)WindowSize.X / width, (float)WindowSize.Y / height);

        GL.Viewport(0, 0, width, height);

        GL.ClearColor(GLExtensions.Color4(new Color(12, 12, 12, 255)));
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit | ClearBufferMask.StencilBufferBit);

        _jobManager.Invoke(JobSchedule.WindowRender, new JobArgs(Client));
        Client.OnRender(args.Time);

        SwapBuffers();

        base.OnRenderFrame(args);
    }

    protected override void OnMouseWheel(MouseWheelEventArgs e)
    {
        _jobManager.Invoke(JobSchedule.WindowMouseScroll, new JobArgs(e.OffsetY));
        Client.OnMouseWheelMoved(GLExtensions.Vector2(e.Offset));

        _logger.Debug("[{0}] Scrolled Delta: {1}", "OpenTK Window", e.Offset);

        base.OnMouseWheel(e);
    }

    protected override unsafe void OnResize(ResizeEventArgs e)
    {
        GLFW.GetFramebufferSize(this.WindowPtr, out var width, out var height);
        ScaleRate = new Vector2((float)width / WindowSize.X, (float)height / WindowSize.Y);

        GL.Viewport(0, 0, width, height);

        _logger.Debug("[{0}] Resized to ({1}, {2})", "OpenTK Window", e.Width, e.Height);

        _jobManager.Invoke(JobSchedule.WindowResize, new JobArgs(Client));
        Client.OnResize(width, height, ScaleRate);

        base.OnResize(e);
    }

    protected override void OnTextInput(TextInputEventArgs e)
    {
        _jobManager.Invoke(JobSchedule.WindowEnterText, new JobArgs(new object[] { e.Unicode }, Client));
        Client.OnTextReceived(e.Unicode, e.AsString);

        _logger.Debug("[{0}] Text Input: {1}", "OpenTK Window", e.AsString);

        base.OnTextInput(e);
    }
}
