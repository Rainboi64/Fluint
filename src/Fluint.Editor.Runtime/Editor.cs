// 
// Editor.c.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Fluint.Editor.Layout;
using Fluint.Layer;
using Fluint.Layer.Configuration;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Graphics.API;
using Fluint.Layer.Localization;
using Fluint.Layer.Miscellaneous;
using Fluint.Layer.Runtime;
using Fluint.Layer.SDK;
using Fluint.Layer.UI;
using Fluint.Layer.Windowing;

namespace Fluint.Runtime;

public static class Editor
{
    private const string BaseFileLocation = "moduleManifest.json";
    private const string VersionDetails = "pre-α 2023.0.0.00";

    public static void Start()
    {
        var timeName = GetTimeOfDayName(DateTime.Now);
        var username = Environment.UserName;

        var moduleDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, BaseFileLocation);
        ConsoleHelper.WriteEmbeddedColorLine(
            $"Good {timeName} [blue]{username}[/blue] Welcome to [red]Fluint[/red] [magenta]{VersionDetails}[/magenta].");

        var manifest = new StartupManifest(Environment.GetCommandLineArgs(), moduleDirectory, VersionDetails);
        var manager = new InstanceManager(manifest);

        ModulePacket packet;
        
        if (manifest.CommandLineArguments.Any(x => x.ToUpper() == "--SDK"))
        {
            var sdk = manager.CreateInstance<SdkInstance>();
            packet = manager.Instances[sdk].Packet;
            
            manager.Instances[sdk].Start();
        }
        else
        {
            var fluint = manager.CreateInstance<EditorInstance>();
            packet = manager.Instances[fluint].Packet;

            // Create an SDK attached to the fluint instance.
            Task.Run(() =>
            {
                var sdk = new SdkInstance();
                sdk.Create(1, manifest, manager.Instances[fluint].ModuleManifest, packet, manager);
                sdk.Start();
            });

            manager.Instances[fluint].Start();
        }
        
        var configurationManager = packet.GetSingleton<IConfigurationManager>();
        var uiConfiguration = configurationManager.Get<UIConfiguration>();

        packet.GetSingleton<ILocalizationManager>().ActiveLanguage = uiConfiguration.Language;

        var provider = packet.CreateScoped<IWindowProvider>();
        var window = packet.CreateScoped<IWindow>();
        
        provider.Adopt(window);
        {
            var layout = new MainWindowLayout(packet);
            layout.Initialize(window.UILayer);
        }
        
        provider.Start();
    }

    private static string GetTimeOfDayName(DateTime time)
    {
        return time.TimeOfDay.TotalHours switch
        {
            < 12 => "morning",
            < 17 => "afternoon",
            > 17 => "evening",
            _ => "day"
        };
    }
}