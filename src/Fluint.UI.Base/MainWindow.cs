//
// MainWindow.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;
using System.Collections.Generic;
using Fluint.Layer.Configuration;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Diagnostics;
using Fluint.Layer.Input;
using Fluint.Layer.Mathematics;
using Fluint.Layer.UI;
using Fluint.Layer.Windowing;
using ImGuiNET;
using Vector4 = System.Numerics.Vector4;

namespace Fluint.UI.Base;

public class MainWindow : IWindow
{
    private readonly IConfigurationManager _configurationManager;

    private readonly List<IPuppet> _ghosts;
    private readonly ILogger _logger;
    private readonly ModulePacket _packet;

    private IWindowProvider _provider;
    
    public MainWindow(ModulePacket packet, ILogger logger, IConfigurationManager configurationManager)
    {
        _configurationManager = configurationManager;
        _packet = packet;
        _logger = logger;

        Controls = new Dictionary<string, IGuiComponent>();
        _ghosts = new List<IPuppet>();
    }

    public T SpawnControl<T>() where T : Control
    {
        var control = _packet.CreateInstance<T>();
        
        Enqueue((() => {
            var name = $"{typeof(T).Name} : {control.GetHashCode()}";
            control.Begin(name, this);
            Controls.Add(name, control);
        }));
        
        return control;
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

    public IInputManager InputManager
    {
        get;
        private set;
    }

    public IDictionary<string, IGuiComponent> Controls
    {
        get;
    }

    public void Close()
    {
        _provider.Close();
    }

    public void OnLoad()
    {
        Title = "Fluint";

        foreach (var ghost in _ghosts)
        {
            _logger.Verbose("[{0}] Loading Ghost: {1}", Title, ghost.ToString());
            ghost.OnLoad();
        }

        var style = ImGui.GetStyle();

        SetStyleFromConfig(style);
        
        var layouts = _packet.GetInstances<ILayout>();
        foreach (var layout in layouts)
        {
            layout.Initialize(this);
        }
    }

    public void OnStart()
    {
        foreach (var ghost in _ghosts)
        {
            ghost.OnStart();
        }
        
        //TODO: Window stuff!
    }

    public void OnRender(double delay)
    {
        foreach (var ghost in _ghosts)
        {
            ghost.OnRender(delay);
        }

        //TODO: Window stuff!
    }

    public void OnUpdate(double delay)
    {
        FrameTime = delay;

        foreach (var ghost in _ghosts)
        {
            ghost.OnUpdate(delay);
        }

        ImGui.DockSpaceOverViewport(ImGui.GetWindowViewport());

        foreach (var control in Controls.Values)
        {
            control.Tick();
        }
        ImGui.ShowDemoWindow();
        
        ImGui.End();

        //TODO: Window stuff!
    }

    public void OnTextReceived(int unicode, string data)
    {
        foreach (var ghost in _ghosts)
        {
            ghost.OnTextReceived(unicode, data);
        }

        //TODO: Window stuff!
    }

    public void OnResize(int width, int height)
    {
        foreach (var ghost in _ghosts)
        {
            ghost.OnResize(width, height);
        }

        //TODO: Window stuff!
    }

    public void OnMouseWheelMoved(Vector2 offset)
    {
        foreach (var ghost in _ghosts)
        {
            ghost.OnMouseWheelMoved(offset);
        }

        //TODO: Window stuff!
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

    public void AdoptGhost<TGhost>() where TGhost : IPuppet
    {
        var ghostType = typeof(TGhost);
        _logger.Verbose("[{0}] Adopting Ghost {1}", Title, ghostType.Name);

        var ghost = (IPuppet)_packet.FetchAndCreateInstance(ghostType);

        ghost.SetPossessed(this);
        _ghosts.Add(ghost);
    }

    private void SetStyleFromConfig(ImGuiStylePtr style)
    {
        ImGui.StyleColorsDark();

        var theme = _configurationManager.Get<ThemeConfiguration>();

        style.WindowPadding = Vector2Vector(theme.WindowPadding);
        style.WindowRounding = theme.WindowRounding;
        style.FramePadding = Vector2Vector(theme.FramePadding);
        style.FrameRounding = theme.FrameRounding;
        style.ItemSpacing = Vector2Vector(theme.ItemSpacing);
        style.ItemInnerSpacing = Vector2Vector(theme.ItemInnerSpacing);
        style.IndentSpacing = theme.IndentSpacing;
        style.ScrollbarSize = theme.ScrollbarSize;
        style.ScrollbarRounding = theme.ScrollbarRounding;
        style.GrabMinSize = theme.GrabMinSize;
        style.GrabRounding = theme.GrabRounding;

        style.Colors[(int)ImGuiCol.Text] = Color2Vector(theme.Text);
        style.Colors[(int)ImGuiCol.TextDisabled] = Color2Vector(theme.TextDisabled);
        style.Colors[(int)ImGuiCol.WindowBg] = Color2Vector(theme.WindowBg);
        style.Colors[(int)ImGuiCol.ChildBg] = Color2Vector(theme.ChildBg);
        style.Colors[(int)ImGuiCol.PopupBg] = Color2Vector(theme.PopupBg);
        style.Colors[(int)ImGuiCol.Border] = Color2Vector(theme.Border);
        style.Colors[(int)ImGuiCol.BorderShadow] = Color2Vector(theme.BorderShadow);
        style.Colors[(int)ImGuiCol.FrameBg] = Color2Vector(theme.FrameBg);
        style.Colors[(int)ImGuiCol.FrameBgHovered] = Color2Vector(theme.FrameBgHovered);
        style.Colors[(int)ImGuiCol.FrameBgActive] = Color2Vector(theme.FrameBgActive);
        style.Colors[(int)ImGuiCol.TitleBg] = Color2Vector(theme.TitleBg);
        style.Colors[(int)ImGuiCol.TitleBgCollapsed] = Color2Vector(theme.TitleBgCollapsed);
        style.Colors[(int)ImGuiCol.TitleBgActive] = Color2Vector(theme.TitleBgActive);
        style.Colors[(int)ImGuiCol.MenuBarBg] = Color2Vector(theme.MenuBarBg);
        style.Colors[(int)ImGuiCol.ScrollbarBg] = Color2Vector(theme.ScrollbarBg);
        style.Colors[(int)ImGuiCol.ScrollbarGrab] = Color2Vector(theme.ScrollbarGrab);
        style.Colors[(int)ImGuiCol.ScrollbarGrabHovered] = Color2Vector(theme.ScrollbarGrabHovered);
        style.Colors[(int)ImGuiCol.ScrollbarGrabActive] = Color2Vector(theme.ScrollbarGrabActive);
        style.Colors[(int)ImGuiCol.CheckMark] = Color2Vector(theme.CheckMark);
        style.Colors[(int)ImGuiCol.SliderGrab] = Color2Vector(theme.SliderGrab);
        style.Colors[(int)ImGuiCol.SliderGrabActive] = Color2Vector(theme.SliderGrabActive);
        style.Colors[(int)ImGuiCol.Button] = Color2Vector(theme.Button);
        style.Colors[(int)ImGuiCol.ButtonHovered] = Color2Vector(theme.ButtonHovered);
        style.Colors[(int)ImGuiCol.ButtonActive] = Color2Vector(theme.ButtonActive);
        style.Colors[(int)ImGuiCol.Header] = Color2Vector(theme.Header);
        style.Colors[(int)ImGuiCol.HeaderHovered] = Color2Vector(theme.HeaderHovered);
        style.Colors[(int)ImGuiCol.HeaderActive] = Color2Vector(theme.HeaderActive);
        style.Colors[(int)ImGuiCol.ResizeGrip] = Color2Vector(theme.ResizeGrip);
        style.Colors[(int)ImGuiCol.ResizeGripHovered] = Color2Vector(theme.ResizeGripHovered);
        style.Colors[(int)ImGuiCol.ResizeGripActive] = Color2Vector(theme.ResizeGripActive);
        style.Colors[(int)ImGuiCol.PlotLines] = Color2Vector(theme.PlotLines);
        style.Colors[(int)ImGuiCol.PlotLinesHovered] = Color2Vector(theme.PlotLinesHovered);
        style.Colors[(int)ImGuiCol.PlotHistogram] = Color2Vector(theme.PlotHistogram);
        style.Colors[(int)ImGuiCol.PlotHistogramHovered] = Color2Vector(theme.PlotHistogramHovered);
        style.Colors[(int)ImGuiCol.TextSelectedBg] = Color2Vector(theme.TextSelectedBg);

    }

    private static Vector4 Color2Vector(Vector4 color)
    {
        return new Vector4(color.X, color.Y, color.Z, color.W);
    }

    private static System.Numerics.Vector2 Vector2Vector(System.Numerics.Vector2 vector)
    {
        return new System.Numerics.Vector2(vector.X, vector.Y);
    }
}