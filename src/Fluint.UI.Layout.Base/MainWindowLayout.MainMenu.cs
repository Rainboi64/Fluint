// 
// MainWindowLayout.MainMenu.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using Fluint.Layer.Functionality;
using Fluint.Layer.Functionality.Common;
using Fluint.Layer.UI;
using Fluint.UI.Layout.Base.Controls;

namespace Fluint.UI.Layout.Base;

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
        _mainMenuFileExitItem.OnClick = new ModularAction { _actionManager.GetAction<IGracefulExit>().Exit };
        _mainMenuFileExitItem.Text = _localizationManager.Fetch("exit");

        // Windows
        _mainMenuWindowsItem = _packet.CreateScoped<IMenuItem>();
        _mainMenu["WindowsMenuItem"] = _mainMenuWindowsItem;
        _mainMenuWindowsItem.Text = _localizationManager.Fetch("window");

        // Windows -> Camera
        _mainMenuWindowsCameraItem = _packet.CreateScoped<IMenuItem>();
        _mainMenuWindowsItem["WindowsMenuCameraItem"] = _mainMenuWindowsCameraItem;
        _mainMenuWindowsCameraItem.OnClick = new ModularAction {
            () => {
                _window.SpawnControl<CameraControl>();
            }
        };
        _mainMenuWindowsCameraItem.Text = _localizationManager.Fetch("camera");

        // Help
        _mainMenuHelpMenuItem = _packet.CreateScoped<IMenuItem>();
        _mainMenu["HelpMenuItem"] = _mainMenuHelpMenuItem;
        _mainMenuHelpMenuItem.Text = _localizationManager.Fetch("help");

        // Help -> About
        _mainMenuHelpAboutItem = _packet.CreateScoped<IMenuItem>();
        _mainMenuHelpMenuItem["HelpAboutMenuItem"] = _mainMenuHelpAboutItem;
        _mainMenuHelpAboutItem.OnClick = new ModularAction {
            () => {
                _window.SpawnControl<AboutControl>();
            }
        };
        _mainMenuHelpAboutItem.Text = _localizationManager.Fetch("about");
    }
}