//
// FluintStarter.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;
using System.Text.Json;
using Fluint.Layer.Tasks;
using System.IO;

namespace Fluint.Layer
{
    public sealed class FluintStarter
    {
        public const string ManifestFile = @"StartupManifest.json";
        public const string DefaultModulesFolder = @"base";

        public static StartupManifest GetManifest()
        {
            StartupManifest manifest;
            if (File.Exists(ManifestFile))
            {
                var jsonData = File.ReadAllText(ManifestFile);
                manifest = JsonSerializer.Deserialize<StartupManifest>(jsonData);
            }
            else
            {
                Console.WriteLine($"Manifest file not fount, creating at {ManifestFile}");
                manifest = new StartupManifest()
                {
                    ActiveFolder = AppDomain.CurrentDomain.BaseDirectory + DefaultModulesFolder
                };
                var jsonData = JsonSerializer.Serialize(manifest, new JsonSerializerOptions() { WriteIndented = true });
                File.WriteAllText(ManifestFile, jsonData);
            }

            return manifest;
        }

        public static void Start()
        {
            var collection = ModulesManager.LoadFolder(GetManifest().ActiveFolder);
            var packet = collection.GenerateModulePacket();

            var taskManager = packet.CreateScoped<ITaskManager>();
            taskManager.Invoke(TaskSchedule.Startup, null);
            taskManager.Invoke(TaskSchedule.Background, null);
        }
    }
}
