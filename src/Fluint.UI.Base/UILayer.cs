// 
// UILayer.cs
// 
// Copyright (C) 2023 Yaman Alhalabi

using System;
using System.Collections.Generic;
using Fluint.Layer.Configuration;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.UI;
using ImGuiNET;

namespace Fluint.UI.Base;

public class UILayer : IUILayer
{
    private readonly IConfigurationManager _configurationManager;
    private readonly ModulePacket _packet;

    private UIContext _context;
    
    public UILayer(IConfigurationManager configurationManager, ModulePacket packet)
    {
        _configurationManager = configurationManager;
        _packet = packet; 
    }
    
    public T SpawnControl<T>() where T : Control
    {
        var control = _packet.CreateInstance<T>();
        _context.Enqueue(() =>
        {
            var name = $"{typeof(T).Name} : {control.GetHashCode()} : {new Random().Next()}";
            control.Begin(name, _context);
            Controls.Add(name, control);
        });
        
        return control;
    }

    public IDictionary<string, IGuiComponent> Controls
    {
        get;
    } = new Dictionary<string, IGuiComponent>();

    public bool Dockspace
    {
        get;
        set;
    } = true;

    public void Render()
    {
        if(Dockspace)
        {
            ImGui.DockSpaceOverViewport(ImGui.GetWindowViewport());
        }

        foreach (var control in Controls.Values)
        {
            control.Tick();
        }

        if (Dockspace)
        {
            ImGui.End();
        }
    }

    public void Load()
    {
        var style = ImGui.GetStyle();
        SetStyleFromConfig(style);
    }

    public void InitializeContext(UIContext context)
    {
        _context = context;
    }

    private void SetStyleFromConfig(ImGuiStylePtr style)
    {
        ImGui.StyleColorsDark();
    
        var theme = _configurationManager.Get<ThemeConfiguration>();
    
        style.WindowPadding = theme.WindowPadding;
        style.WindowRounding = theme.WindowRounding;
        style.TabRounding = theme.TabRounding;
        style.FramePadding = theme.FramePadding;
        style.FrameRounding = theme.FrameRounding;
        style.ItemSpacing = theme.ItemSpacing;
        style.ItemInnerSpacing = theme.ItemInnerSpacing;
        style.IndentSpacing = theme.IndentSpacing;
        style.ScrollbarSize = theme.ScrollbarSize;
        style.ScrollbarRounding = theme.ScrollbarRounding;
        style.GrabMinSize = theme.GrabMinSize;
        style.GrabRounding = theme.GrabRounding;
    
        style.Colors[(int)ImGuiCol.Text] = theme.Text;
        style.Colors[(int)ImGuiCol.TextDisabled] = theme.TextDisabled;
        style.Colors[(int)ImGuiCol.WindowBg] = theme.WindowBg;
        style.Colors[(int)ImGuiCol.ChildBg] = theme.ChildBg;
        style.Colors[(int)ImGuiCol.PopupBg] = theme.PopupBg;
        style.Colors[(int)ImGuiCol.Border] = theme.Border;
        style.Colors[(int)ImGuiCol.BorderShadow] = theme.BorderShadow;
        style.Colors[(int)ImGuiCol.FrameBg] = theme.FrameBg;
        style.Colors[(int)ImGuiCol.FrameBgHovered] = theme.FrameBgHovered;
        style.Colors[(int)ImGuiCol.FrameBgActive] = theme.FrameBgActive;
        style.Colors[(int)ImGuiCol.TitleBg] = theme.TitleBg;
        style.Colors[(int)ImGuiCol.TitleBgCollapsed] = theme.TitleBgCollapsed;
        style.Colors[(int)ImGuiCol.TitleBgActive] = theme.TitleBgActive;
        style.Colors[(int)ImGuiCol.MenuBarBg] = theme.MenuBarBg;
        style.Colors[(int)ImGuiCol.ScrollbarBg] = theme.ScrollbarBg;
        style.Colors[(int)ImGuiCol.ScrollbarGrab] = theme.ScrollbarGrab;
        style.Colors[(int)ImGuiCol.ScrollbarGrabHovered] = theme.ScrollbarGrabHovered;
        style.Colors[(int)ImGuiCol.ScrollbarGrabActive] = theme.ScrollbarGrabActive;
        style.Colors[(int)ImGuiCol.CheckMark] = theme.CheckMark;
        style.Colors[(int)ImGuiCol.SliderGrab] = theme.SliderGrab;
        style.Colors[(int)ImGuiCol.SliderGrabActive] = theme.SliderGrabActive;
        style.Colors[(int)ImGuiCol.Button] = theme.Button;
        style.Colors[(int)ImGuiCol.ButtonHovered] = theme.ButtonHovered;
        style.Colors[(int)ImGuiCol.ButtonActive] = theme.ButtonActive;
        style.Colors[(int)ImGuiCol.Header] = theme.Header;
        style.Colors[(int)ImGuiCol.HeaderHovered] = theme.HeaderHovered;
        style.Colors[(int)ImGuiCol.HeaderActive] = theme.HeaderActive;
        style.Colors[(int)ImGuiCol.ResizeGrip] = theme.ResizeGrip;
        style.Colors[(int)ImGuiCol.ResizeGripHovered] = theme.ResizeGripHovered;
        style.Colors[(int)ImGuiCol.ResizeGripActive] = theme.ResizeGripActive;
        style.Colors[(int)ImGuiCol.PlotLines] = theme.PlotLines;
        style.Colors[(int)ImGuiCol.PlotLinesHovered] = theme.PlotLinesHovered;
        style.Colors[(int)ImGuiCol.PlotHistogram] = theme.PlotHistogram;
        style.Colors[(int)ImGuiCol.PlotHistogramHovered] = theme.PlotHistogramHovered;
        style.Colors[(int)ImGuiCol.TextSelectedBg] = theme.TextSelectedBg;
        style.Colors[(int)ImGuiCol.Separator] = theme.Separator;
    }
    
}