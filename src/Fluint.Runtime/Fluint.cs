// 
// Fluint.c.cs
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

public class Fluint
{
    private const string BaseFileLocation = "base";
    private const string VersionDetails = "pre-α 2022.5.1.00";

    public void Start()
    {
        var timeName = GetTimeOfDayName(DateTime.Now);
        var username = Environment.UserName;
        username = $"{username[0].ToString().ToUpper()}{new string(username.Skip(1).ToArray())}";

        var moduleDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, BaseFileLocation);
        ConsoleHelper.WriteEmbeddedColorLine(
            $"Good {timeName} [blue]{username}[/blue] Welcome to [red]Fluint[/red] [magenta]{VersionDetails}[/magenta].\nKick-starting module directory [green]\"{moduleDirectory}\"[/green]");

        var manifest = new StartupManifest(Environment.GetCommandLineArgs(), moduleDirectory, VersionDetails);
        var manager = new InstanceManager(manifest);

        manager.CreateInstance<FluintInstance>();

        // Create an SDK attached to the fluint instance.
        Task.Run(() => {
            var sdk = new SdkInstance();
            sdk.Create(1, manifest, manager.Instances[0].Packet, null);
            sdk.Start();
        });

        manager.StartAll();
    }

    private static string GetTimeOfDayName(DateTime time)
    {
        return time.TimeOfDay.TotalHours switch {
            < 12 => "morning",
            < 17 => "afternoon",
            > 17 => "evening",
            _ => "day"
        };
    }
}