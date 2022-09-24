using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Miscellaneous;

namespace Fluint.Layer.Runtime
{
    public class InstanceManager
    {
        private readonly ModuleCollection _collection;
        private readonly Dictionary<int, IRuntime> _instances = new();
        private readonly StartupManifest _manifest;
        private readonly ModuleManifest _moduleManifest;

        public InstanceManager(StartupManifest manifest)
        {
            _manifest = manifest;

            if (File.Exists(manifest.ModuleManifest))
            {
                var json = File.ReadAllText(manifest.ModuleManifest);
                _moduleManifest = JsonSerializer.Deserialize<ModuleManifest>(json, new JsonSerializerOptions {
                    ReadCommentHandling = JsonCommentHandling.Skip,
                    AllowTrailingCommas = true
                });

                _collection = ModulesManager.LoadManifest(_moduleManifest);
            }
            else
            {
                ConsoleHelper.WriteInfo(
                    $"The path \"{manifest.ModuleManifest}\" for the module manifest wasn't found, loading from 'base' and creating ./moduleManifest.json");
                _moduleManifest = ModulesManager.GenerateManifest("base");

                File.WriteAllTextAsync(
                    "moduleManifest.json",
                    JsonSerializer.Serialize(_moduleManifest, new JsonSerializerOptions { WriteIndented = true }));

                _collection = ModulesManager.LoadManifest(_moduleManifest);
            }
        }

        public IReadOnlyDictionary<int, IRuntime> Instances => _instances;

        public int CreateInstance<T>() where T : IRuntime, new()
        {
            var instance = new T();
            var id = _instances.Count;
            instance.Create(id, _manifest, _moduleManifest, _collection.GenerateModulePacket(instance), this);
            _instances.Add(id, instance);

            return instance.Id;
        }

        public void Start(int id)
        {
            _instances[id].Start();
        }

        public void Kill(int id)
        {
            _instances[id].Kill();
        }

        public void StartAll()
        {
            foreach (var instance in _instances.Values)
            {
                var thread = new Thread(() => {
                    instance.Start();
                });
                thread.Start();
            }
        }

        public void KillAll()
        {
            foreach (var instance in _instances.Values)
            {
                instance.Kill();
            }
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            foreach (var instance in _instances)
            {
                builder.AppendLine(instance.ToString());
            }

            return builder.ToString();
        }
    }
}