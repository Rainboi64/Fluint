// 
// LayoutInitialization.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using Fluint.Layer.UI;
using Fluint.Layer.Windowing;

namespace Fluint.Editor.Layout.Base;

public partial class MainWindowLayout
{
    // MainMenu
    private IMainMenu _mainMenu;

    private IMenuItem _mainMenuDebugItem;
    private IMenuItem _mainMenuDebugMetricsItem;

    private IMenuItem _mainMenuFileExitItem;

    private IMenuItem _mainMenuFileMenuItem;
    private IMenuItem _mainMenuHelpAboutItem;

    private IMenuItem _mainMenuHelpMenuItem;
    private IMenuItem _mainMenuToolsCameraItem;

    private IMenuItem _mainMenuToolsItem;
    private IMenuItem _mainMenuToolsToolboxItem;

    private IToast _toast;

    public void Initialize(IWindow window)
    {
        _window = window;

        _toast = _packet.GetSingleton<IToast>();
        _window.Controls["toast"] = _toast;
        _toast.Begin("toast");

        MainMenu();
    }
}