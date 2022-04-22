//
// Program.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;
using System.IO;
using System.Threading.Tasks;
using Fluint.Layer;
using Fluint.Layer.Miscellaneous;
using Fluint.Layer.Runtime;
using Fluint.Layer.SDK;

namespace Fluint.Runtime
{
    class Program
    {
        static string GetTimeOfDayName(DateTime time)
        {
            if (time.TimeOfDay.TotalHours < 12)
            {
                return "morning";
            }

            if (time.TimeOfDay.TotalHours < 17)
            {
                return "afternoon";
            }

            if (time.TimeOfDay.TotalHours > 17)
            {
                return "evening";
            }

            return "day";
        }

        static void Main(string[] args)
        {
            var timeName = GetTimeOfDayName(DateTime.Now);
            var username = Environment.UserName;

            var moduleDirectiory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "base");

            ConsoleHelper.WriteEmbeddedColorLine(
                $"Good {timeName} [blue]{username}[/blue] Welcome to [red]Fluint[/red].\nKickstarting module directory [green]\"{moduleDirectiory}\"[/green]");

            var manifest = new StartupManifest(args, moduleDirectiory);
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
    }
}