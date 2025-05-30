//
// MainWindow.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using Fluint.Layer.Configuration;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Diagnostics;
using Fluint.Layer.Graphics.API;
using Fluint.Layer.Input;
using Fluint.Layer.Mathematics;
using Fluint.Layer.UI;
using Fluint.Layer.Windowing;

namespace Fluint.Windowing.Base;

public class Window : IWindow
{
    private readonly IConfigurationManager _configurationManager;
    private readonly ILogger _logger;
    private readonly ModulePacket _packet;

    private readonly List<IPuppet> _puppets;

    private IWindowProvider _provider;

    public Window(ModulePacket packet, ILogger logger, IConfigurationManager configurationManager, IUILayer layer)
    {
        _configurationManager = configurationManager;
        _packet = packet;
        _logger = logger;
        
        UILayer = layer;
        UILayer.InitializeContext(new UIContext(this, UILayer));
        
        Profiler = packet.CreateScoped<IFrameProfiler>();

        _puppets = new List<IPuppet>();
    }


    public event EventHandler? Load;
    public event EventHandler<RenderEvent>? Render;
    public event EventHandler<RenderEvent>? Update;
    public event EventHandler<ResizeEvent>? Resize;

    public IUILayer UILayer
    {
        get;
    }

    public IFrameProfiler Profiler
    {
        get;
    }

    public double FrameTime
    {
        get;
        private set;
    }

    public string Title
    {
        get => _provider.WindowTitle;
        set => _provider.WindowTitle = value;
    }

    public Vector2i ScreenSize => _provider.ScreenSize;
    public Vector2 ScaleRate
    {
        get;
        private set;
    }

    public bool VSync
    {
        get => _provider.WindowVSync;
        set => _provider.WindowVSync = value;
    }

    public Vector2i Size
    {
        get => _provider.WindowSize;
        set => _provider.WindowSize = value;
    }

    public Vector2i Location
    {
        get => _provider.WindowLocation;
        set => _provider.WindowLocation = value;
    }

    public IInputManager? InputManager
    {
        get;
        private set;
    }

    public void Close()
    {
        _provider.Close();
    }

    public void OnLoad()
    {
        Profiler.Begin("Window Load");

        Title = "Fluint";

        foreach (var ghost in _puppets)
        {
            _logger.Verbose("[{0}] Loading Puppet: {1}", Title, ghost.ToString());
            ghost.OnLoad();
        }
        
        UILayer.Load();
        
        Load?.Invoke(this, EventArgs.Empty);

        Profiler.End("Window Load");
    }

    public void OnStart()
    {
        foreach (var ghost in _puppets)
        {
            ghost.OnStart();
        }
    }

    public void OnRender(double delay)
    {
        Profiler.Begin("Render");

        foreach (var ghost in _puppets)
        {
            ghost.PreRender(delay);
        }

        Render?.Invoke(this, new RenderEvent(delay));
        
        foreach (var ghost in _puppets)
        {
            ghost.PostRender(delay);
        }
        
        Profiler.End("Render");
    }

    public void OnUpdate(double delay)
    {
        Profiler.Begin("Update");

        FrameTime = delay;

        foreach (var ghost in _puppets)
        {
            ghost.PreUpdate(delay);
        }
       
        UILayer.Render();

        Update?.Invoke(this, new RenderEvent(delay));
        
        foreach (var ghost in _puppets)
        {
            ghost.PostRender(delay);
        }

        Profiler.End("Update");
    }

    public void OnTextReceived(int unicode, string data)
    {
        foreach (var ghost in _puppets)
        {
            ghost.OnTextReceived(unicode, data);
        }
    }

    public void OnResize(int width, int height, Vector2 scaleRate)
    {
        ScaleRate = scaleRate;
        foreach (var ghost in _puppets)
        {
            ghost.OnResize(width, height);
        }

        Resize?.Invoke(this, new ResizeEvent(new Vector2i(width, height)));
    }

    public void OnMouseWheelMoved(Vector2 offset)
    {
        foreach (var ghost in _puppets)
        {
            ghost.OnMouseWheelMoved(offset);
        }
    }

    public void SetProvider(in IWindowProvider provider)
    {
        _provider = provider;

        InputManager = _packet.CreateScoped<IInputManager>();
        InputManager.Load(provider);
    }

    public void Enqueue(Action action)
    {
        _provider.FrameQueue.Enqueue(action);
    }

    public void Puppet<T>() where T : IPuppet
    {
        var type = typeof(T);
        _logger.Verbose("[{0}] Adopting Puppet {1}", Title, type.Name);

        var puppet = (IPuppet)_packet.FetchAndCreateInstance(type);

        puppet.SetPossessed(this);
        _puppets.Add(puppet);
    }
}