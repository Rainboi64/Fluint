// 
// Editor.c.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Fluint.Layer;
using Fluint.Layer.Miscellaneous;
using Fluint.Layer.Runtime;
using Fluint.Layer.SDK;

namespace Fluint.Runtime;

public class Editor
{
    private const string BaseFileLocation = "moduleManifest.json";
    private const string VersionDetails = "pre-α 2023.0.0.00";

    public static void Start()
    {
        var timeName = GetTimeOfDayName(DateTime.Now);
        var username = Environment.UserName;
        username = $"{username[0].ToString().ToUpper()}{new string(username.Skip(1).ToArray())}";

        var moduleDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, BaseFileLocation);
        ConsoleHelper.WriteEmbeddedColorLine(
            $"Good {timeName} [blue]{username}[/blue] Welcome to [red]Fluint[/red] [magenta]{VersionDetails}[/magenta].");

        var manifest = new StartupManifest(Environment.GetCommandLineArgs(), moduleDirectory, VersionDetails);
        var manager = new InstanceManager(manifest);

        if (manifest.CommandLineArguments.Any(x => x.ToUpper() == "--SDK"))
        {
            var sdk = manager.CreateInstance<SdkInstance>();
            manager.Instances[sdk].Start();
        }
        else
        {
            var fluint = manager.CreateInstance<Instance>();

            // Create an SDK attached to the fluint instance.
            Task.Run(() =>
            {
                var sdk = new SdkInstance();
                sdk.Create(1, manifest, manager.Instances[0].ModuleManifest, manager.Instances[0].Packet, manager);
                sdk.Start();
            });

            manager.Instances[fluint].Start();
        }
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