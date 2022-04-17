//
// Program.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;
using System.IO;
using Fluint.Layer;
using Fluint.Layer.Runtime;
using Fluint.Layer.SDK;
using Fluint.Layer.Miscellaneous;

namespace Fluint.Runtime
{
    class Program
    {
        static string GetTimeOfDayName(DateTime time)
        {
            if (time.TimeOfDay.TotalHours < 12)
            {
                return "Morning";
            }

            if (time.TimeOfDay.TotalHours < 17)
            {
                return "Afternoon";
            }

            if (time.TimeOfDay.TotalHours > 17)
            {
                return "Evening";
            }

            return "Day";
        }

        static void Main(string[] args)
        {
            var timeName = GetTimeOfDayName(DateTime.Now);
            var username = Environment.UserName;

            ConsoleHelper.WriteWrappedHeader($"Good {timeName} {username}! Kickstarting base");
            var moduleDirectiory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "base");
            var manager = new InstanceManager(new StartupManifest(args, moduleDirectiory));

            manager.CreateInstance<FluintInstance>();
            manager.CreateInstance<SDKInstance>();
            manager.StartAll();
        }
    }
}
