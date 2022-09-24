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
    private IMenuItem _mainMenuDebugUIDemoItem;
    private IMenuItem _mainMenuFileExitItem;

    private IMenuItem _mainMenuFileMenuItem;
    private IMenuItem _mainMenuHelpAboutItem;

    private IMenuItem _mainMenuHelpMenuItem;
    private IMenuItem _mainMenuToolsCameraItem;

    private IMenuItem _mainMenuToolsItem;
    private IMenuItem _mainMenuToolsToolboxItem;

    public void Initialize(IWindow window)
    {
        _window = window;

        MainMenu();
    }
}