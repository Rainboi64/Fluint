// 
// MetricsControl.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Localization;
using Fluint.Layer.UI;
using Fluint.Layer.Windowing;

namespace Fluint.Editor.Layout.Base.Controls;

public class MetricsControl : Control
{
    private readonly ITextLabel _metricsLabel;
    private readonly string _template;
    private readonly ITextLabel _versionLabel;
    private IWindow _window;

    public MetricsControl(ModulePacket packet)
    {
        var localizationManager = packet.GetSingleton<ILocalizationManager>();
        _template = localizationManager.Fetch("fps_frametime");

        var container = packet.CreateScoped<IContainer>();

        _versionLabel = packet.CreateScoped<ITextLabel>();
        _metricsLabel = packet.CreateScoped<ITextLabel>();

        container.Title = localizationManager.Fetch("metrics");
        ;
        _versionLabel.Text = $"Fluint - {packet.CurrentRuntime.StartupManifest.VersionDetails}";

        container["metricsLabel"] = _metricsLabel;
        container["versionLabel"] = _versionLabel;

        Children.Add(container);
    }

    public override void Begin(string name, IWindow parent)
    {
        _window = parent;
        base.Begin(name, parent);
    }

    public override void Tick()
    {
        _metricsLabel.Text = string.Format(_template, 1.0 / _window.FrameTime, _window.FrameTime * 1000.0);
        base.Tick();
    }
}