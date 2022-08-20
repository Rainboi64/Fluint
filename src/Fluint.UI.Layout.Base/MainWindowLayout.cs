// 
// MainWindowLayout.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Functionality;
using Fluint.Layer.Localization;
using Fluint.Layer.UI;
using Fluint.Layer.Windowing;

namespace Fluint.UI.Layout.Base;

public partial class MainWindowLayout : ILayout
{
    private IWindow _window;
    private readonly ModulePacket _packet;
    private readonly ILocalizationManager _localizationManager;
    private readonly IActionManager _actionManager;

    public MainWindowLayout(ModulePacket packet)
    {
        _packet = packet;
        _actionManager = packet.CreateScoped<IActionManager>();
        _localizationManager = packet.GetSingleton<ILocalizationManager>();
    }
}