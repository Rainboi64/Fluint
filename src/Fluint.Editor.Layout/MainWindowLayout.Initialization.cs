// 
// LayoutInitialization.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using System;
using System.IO;
using Fluint.Layer.UI;

namespace Fluint.Editor.Layout;

public partial class MainWindowLayout
{
    private const string LayoutDirectory = "layouts/";
    private readonly string _directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, LayoutDirectory);

    // MainMenu
    private IMainMenu _mainMenu;

    private IMenuItem _mainMenuDebugItem;
    private IMenuItem _mainMenuDebugMetricsItem;

    private IMenuItem _mainMenuFileExitItem;

    private IMenuItem _mainMenuFileMenuItem;
    private IMenuItem _mainMenuHelpAboutItem;

    private IMenuItem _mainMenuHelpMenuItem;
    private IMenuItem _mainMenuToolsItem;
    private IMenuItem _mainMenuToolsCameraItem;
    private IMenuItem _mainMenuToolsSceneHierarchyItem;
    private IMenuItem _mainMenuToolsToolboxItem;

    private IMenuItem _mainMenuWindowItem;
    private IMenuItem _mainMenuWindowNotificationsItem;
    
    private IToast _toast;

    private FileSystemWatcher _watcher;

    public void Initialize(IUILayer layer)
    {
        _layer = layer;

        _watcher = new FileSystemWatcher(_directory);
        _watcher.Changed += WatcherOnChanged;
        
        if (Directory.Exists(_directory))
        {
            var files = Directory.GetFiles(_directory);
            _watcher.Filter = "*.layout.xml";
            _watcher.NotifyFilter = NotifyFilters.LastWrite;

            _watcher.EnableRaisingEvents = true;
                
            foreach (var path in files)
            {
                if (!path.EndsWith(".layout.xml"))
                {
                    continue;
                }
                
                var file = Path.GetFileNameWithoutExtension(path);
                var node = _packet.CreateScoped<IXmlNode>();
                node.Xml = File.ReadAllText(path);
                _layer.Controls[file] = node;
                node.Begin(file);
            }
        }
        else
        {
            _logger.Warning("layouts folder not found {0}", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, LayoutDirectory));
        }
        
        _toast = _packet.CreateScoped<IToast>();
        _layer.Controls["toast"] = _toast;
        _toast.Begin("toast");

        MainMenu();
    }

    private void WatcherOnChanged(object sender, FileSystemEventArgs e)
    {
        var files = Directory.GetFiles(_directory);
        foreach (var path in files)
        {
            if (!path.EndsWith(".layout.xml"))
            {
                continue;
            }

            var file = Path.GetFileNameWithoutExtension(path);

            if (!_layer.Controls.ContainsKey(file))
            {
                continue;
            }

            if (_layer.Controls[file] is not IXmlNode { Live: true } node)
            {
                continue;
            }

            _logger.Information("[{0}] Reloading {1}", "MainWindowLayout", file);

            var newNode = _packet.CreateScoped<IXmlNode>();
            newNode.Xml = File.ReadAllText(path);
            _layer.Controls[file] = newNode;
            newNode.Begin(file);
        }
    }
}