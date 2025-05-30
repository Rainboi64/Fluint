// 
// MainWindowLayout.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using System;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Diagnostics;
using Fluint.Layer.Functionality;
using Fluint.Layer.Localization;
using Fluint.Layer.UI;
using Fluint.Layer.Windowing;

namespace Fluint.Editor.Layout.Base;

public partial class MainWindowLayout : ILayout
{
    private readonly IActionManager _actionManager;
    private readonly ILocalizationManager _localizationManager;
    private readonly ILogger _logger;
    private readonly ModulePacket _packet;

    private readonly Random _random = new();
    private IUILayer _layer;

    public MainWindowLayout(ModulePacket packet)
    {
        _packet = packet;
        _actionManager = packet.CreateScoped<IActionManager>();
        _localizationManager = packet.GetSingleton<ILocalizationManager>();
        _logger = packet.GetSingleton<ILogger>();
    }
}