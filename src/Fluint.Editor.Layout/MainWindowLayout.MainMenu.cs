// 
// MainWindowLayout.MainMenu.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using System;
using System.IO;
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
        _layer.Controls["MainMenu"] = _mainMenu;

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
                _layer.SpawnControl<SureExitModal>();
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
                _layer.SpawnControl<CameraControl>();
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
                _layer.SpawnControl<ToolboxControl>();
            }
        };
        _mainMenuToolsToolboxItem.Text = _localizationManager.Fetch("toolbox");

        // Tools -> Scene Hierarchy
        _mainMenuToolsSceneHierarchyItem = _packet.CreateScoped<IMenuItem>();
        _mainMenuToolsItem["ToolsMenuSceneHierarchyItem"] = _mainMenuToolsSceneHierarchyItem;
        _mainMenuToolsSceneHierarchyItem.OnClick = new ModularAction
        {
            () =>
            {
                _layer.SpawnControl<SceneHierarchyControl>();
            }
        };
        _mainMenuToolsSceneHierarchyItem.Text = _localizationManager.Fetch("scene_heirarchy");

        // Window
        _mainMenuWindowItem = _packet.CreateScoped<IMenuItem>();
        _mainMenu["WindowMenuItem"] = _mainMenuWindowItem;
        _mainMenuWindowItem.Text = _localizationManager.Fetch("window");
        
        // Window -> Notifications
        _mainMenuWindowNotificationsItem = _packet.CreateScoped<IMenuItem>();
        _mainMenuWindowItem["WindowMenuNotificationItem"] = _mainMenuWindowNotificationsItem;
        _mainMenuWindowNotificationsItem.Text = _localizationManager.Fetch("notifications");
        _mainMenuWindowNotificationsItem.OnClick = new ModularAction
        {
            () =>
            {
                _layer.SpawnControl<NotificationsControl>();
            }
        };

        // Debug
        _mainMenuDebugItem = _packet.CreateScoped<IMenuItem>();
        _mainMenu["DebugMenuItem"] = _mainMenuDebugItem;
        _mainMenuDebugItem.Text = _localizationManager.Fetch("debug");

        // Debug -> Metrics

        _layer.SpawnControl<UIDemoControl>();
        
        _mainMenuDebugMetricsItem = _packet.CreateScoped<IMenuItem>();
        _mainMenuDebugItem["DebugMetricsMenuItem"] = _mainMenuDebugMetricsItem;
        _mainMenuDebugMetricsItem.Text = _localizationManager.Fetch("metrics");
        _mainMenuDebugMetricsItem.OnClick = new ModularAction
        {
            () =>
            {
            //     _window.SpawnControl<MetricsControl>();

            if (!Directory.Exists(LayoutDirectory))
            {
                return;
            }

            var files = Directory.GetFiles(LayoutDirectory);
                foreach (var path in files)
                {
                    var file = Path.GetFileNameWithoutExtension(path);
                
                    var node = _packet.CreateScoped<IXmlNode>();
                    node.Xml = File.ReadAllText(path);
                    _layer.Controls[file] = node;
                    node.Begin(file);
                }
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
                _layer.SpawnControl<AboutControl>();
            }
        };
        _mainMenuHelpAboutItem.Text = _localizationManager.Fetch("about");
    }
}