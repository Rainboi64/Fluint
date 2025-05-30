// 
// MetricsControl.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using System;
using System.Text;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Diagnostics;
using Fluint.Layer.Localization;
using Fluint.Layer.UI;
using Fluint.Layer.Windowing;

namespace Fluint.Editor.Layout.Controls;

public class MetricsControl : Control
{
    private readonly string _template;
    private readonly ITextLabel _metricsLabel;
    private readonly ITextLabel _debugLabel;
    private readonly IDebugRepository _repository;
    
    private IWindow _window;

    public MetricsControl(ModulePacket packet)
    {
        _repository = packet.GetSingleton<IDebugRepository>();
        
        var localizationManager = packet.GetSingleton<ILocalizationManager>();
        _template = localizationManager.Fetch("fps_frametime");

        var container = packet.CreateScoped<IContainer>();

        var versionLabel = packet.CreateScoped<ITextLabel>();
        _metricsLabel = packet.CreateScoped<ITextLabel>();
        _debugLabel = packet.CreateScoped<ITextLabel>();


        container.Title = localizationManager.Fetch("metrics");

        versionLabel.Text = $"Fluint - {packet.CurrentRuntime.StartupManifest.VersionDetails}";
        
        container["metricsLabel"] = _metricsLabel;
        container["versionLabel"] = versionLabel;
        container["debugLabel"] = _debugLabel;

        Children.Add(container);
    }

    public override void Begin(string name, UIContext context)
    {
        _window = context.Window;
        base.Begin(name, context);
    }

    public override void Tick()
    {
        _metricsLabel.Text = string.Format(_template, 1.0 / _window.FrameTime, _window.FrameTime * 1000.0,
            GC.GetTotalMemory(true) / 1000);

        var stringBuilder = new StringBuilder();
        foreach (var (key, value) in _repository.Items)
        {
            stringBuilder.AppendLine();
            stringBuilder.AppendLine(key);
            stringBuilder.AppendLine(value);
        }


        _debugLabel.Text = stringBuilder.ToString();
        base.Tick();
    }
}