// 
// MainWindowLayout.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Functionality;
using Fluint.Layer.Localization;
using Fluint.Layer.UI;
using Fluint.Layer.Windowing;

namespace Fluint.Editor.Layout.Base;

public partial class MainWindowLayout : ILayout
{
    private readonly IActionManager _actionManager;
    private readonly ILocalizationManager _localizationManager;
    private readonly ModulePacket _packet;
    private IWindow _window;

    public MainWindowLayout(ModulePacket packet)
    {
        _packet = packet;
        _actionManager = packet.CreateScoped<IActionManager>();
        _localizationManager = packet.GetSingleton<ILocalizationManager>();
    }
}