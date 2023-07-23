// 
// MainWindowLayout.MainMenu.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using Fluint.Editor.Layout.Base.Controls;
using Fluint.Editor.Layout.Base.Controls.Modals;
using Fluint.Layer.Functionality;
using Fluint.Layer.UI;

namespace Fluint.Editor.Layout.Base;

public partial class MainWindowLayout
{
    private void MainMenu()
    {
        // Main Menu
        _mainMenu = _packet.CreateScoped<IMainMenu>();
        _window.Controls["MainMenu"] = _mainMenu;

        // File
        _mainMenuFileMenuItem = _packet.CreateScoped<IMenuItem>();
        _mainMenu["FileMenuItem"] = _mainMenuFileMenuItem;
        _mainMenuFileMenuItem.Text = _localizationManager.Fetch("file");

        // File -> Exit
        _mainMenuFileExitItem = _packet.CreateScoped<IMenuItem>();
        _mainMenuFileMenuItem["FileExitMenuItem"] = _mainMenuFileExitItem;
        _mainMenuFileExitItem.OnClick = new ModularAction
        {
            () =>
            {
                _window.SpawnControl<SureExitModal>();
            }
        };
        _mainMenuFileExitItem.Text = _localizationManager.Fetch("exit");

        // Tools
        _mainMenuToolsItem = _packet.CreateScoped<IMenuItem>();
        _mainMenu["ToolsMenuItem"] = _mainMenuToolsItem;
        _mainMenuToolsItem.Text = _localizationManager.Fetch("tools");

        // Tools -> Camera
        _mainMenuToolsCameraItem = _packet.CreateScoped<IMenuItem>();
        _mainMenuToolsItem["ToolsMenuCameraItem"] = _mainMenuToolsCameraItem;
        _mainMenuToolsCameraItem.OnClick = new ModularAction
        {
            () =>
            {
                _window.SpawnControl<CameraControl>();
            }
        };
        _mainMenuToolsCameraItem.Text = _localizationManager.Fetch("camera");

        // Tools -> Toolbox
        _mainMenuToolsToolboxItem = _packet.CreateScoped<IMenuItem>();
        _mainMenuToolsItem["ToolsMenuToolboxItem"] = _mainMenuToolsToolboxItem;
        _mainMenuToolsToolboxItem.OnClick = new ModularAction
        {
            () =>
            {
                _window.SpawnControl<ToolboxControl>();
            }
        };
        _mainMenuToolsToolboxItem.Text = _localizationManager.Fetch("toolbox");

        // Debug
        _mainMenuDebugItem = _packet.CreateScoped<IMenuItem>();
        _mainMenu["DebugMenuItem"] = _mainMenuDebugItem;
        _mainMenuDebugItem.Text = _localizationManager.Fetch("debug");

        // Debug -> Metrics

        _mainMenuDebugMetricsItem = _packet.CreateScoped<IMenuItem>();
        _mainMenuDebugItem["DebugMetricsMenuItem"] = _mainMenuDebugMetricsItem;
        _mainMenuDebugMetricsItem.Text = _localizationManager.Fetch("metrics");
        _mainMenuDebugMetricsItem.OnClick = new ModularAction
        {
            () =>
            {
                _window.SpawnControl<MetricsControl>();
            }
        };

        // Help
        _mainMenuHelpMenuItem = _packet.CreateScoped<IMenuItem>();
        _mainMenu["HelpMenuItem"] = _mainMenuHelpMenuItem;
        _mainMenuHelpMenuItem.Text = _localizationManager.Fetch("help");

        // Help -> About
        _mainMenuHelpAboutItem = _packet.CreateScoped<IMenuItem>();
        _mainMenuHelpMenuItem["HelpAboutMenuItem"] = _mainMenuHelpAboutItem;
        _mainMenuHelpAboutItem.OnClick = new ModularAction
        {
            () =>
            {
                _window.SpawnControl<AboutControl>();
            }
        };
        _mainMenuHelpAboutItem.Text = _localizationManager.Fetch("about");
    }
}