// 
// Editor.c.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using Fluint.Layer;
using Fluint.Layer.Graphics.API;
using Fluint.Layer.Runtime;
using Fluint.Layer.Windowing;

namespace Game;

public static class BehindTheSun
{
    private const string BaseFileLocation = "moduleManifest.json";
    private const string VersionDetails = "pre-α 2023.0.0.00";

    public static void Start()
    {
        var moduleDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, BaseFileLocation);

        var manifest = new StartupManifest(Environment.GetCommandLineArgs(), moduleDirectory, VersionDetails);
        var manager = new InstanceManager(manifest);

        var fluint = manager.CreateInstance<GameInstance>();
        var packet = manager.Instances[fluint].Packet;
        
        manager.Instances[fluint].Start();
        
        var provider = packet.CreateScoped<IWindowProvider>();
        var window = packet.CreateScoped<IWindow>();
        
        provider.Adopt(window);
        window.Puppet<GameWindow>(); 
        
        provider.Start();
    }
}