// 
// LayoutInitialization.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using Fluint.Layer.UI;
using Fluint.Layer.Windowing;

namespace Fluint.UI.Layout.Base;

public partial class MainWindowLayout
{
    // MainMenu
    private IMainMenu _mainMenu;

    private IMenuItem _mainMenuFileMenuItem;
    private IMenuItem _mainMenuFileExitItem;

    private IMenuItem _mainMenuWindowsItem;
    private IMenuItem _mainMenuWindowsCameraItem;

    private IMenuItem _mainMenuHelpMenuItem;
    private IMenuItem _mainMenuHelpAboutItem;

    private IViewport _viewport;

    public void Initialize(IWindow window)
    {
        _window = window;

        MainMenu();
    }
}