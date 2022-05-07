using System.Collections.Generic;
using System.Text;
using System.Threading;
using Fluint.Layer.DependencyInjection;

namespace Fluint.Layer.Runtime
{
    public class InstanceManager
    {
        private ModuleCollection _collection;

        private Dictionary<int, IRuntime> _instances = new Dictionary<int, IRuntime>();
        private StartupManifest _manifest;

        public InstanceManager(StartupManifest manifest)
        {
            _manifest = manifest;
            _collection = ModulesManager.LoadFolder(manifest.ModulesDirectory);
        }

        public IReadOnlyDictionary<int, IRuntime> Instances => _instances;

        public int CreateInstance<T>() where T : IRuntime, new()
        {
            var instance = new T();
            var id = _instances.Count;
            instance.Create(id, _manifest, _collection.GenerateModulePacket(instance), this);
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