using System.Runtime.InteropServices;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Diagnostics;
using Fluint.Layer.Tasks;

namespace Fluint.Layer.Runtime
{
    public class EditorInstance : IRuntime
    {
        private ILogger _logger;

        public ITaskManager TaskManager
        {
            get;
            private set;
        }

        public void Create(int id, StartupManifest manifest, ModulePacket packet, InstanceManager parent)
        {
            Packet = packet;
            Parent = parent;
            Id = id;
            Manifest = manifest;

            _logger = Packet.GetSingleton<ILogger>();
            TaskManager = Packet.CreateScoped<ITaskManager>();
        }

        public int Id
        {
            get;
            private set;
        }

        public InstanceManager Parent
        {
            get;
            private set;
        }

        public StartupManifest Manifest
        {
            get;
            private set;
        }

        public ModulePacket Packet
        {
            get;
            private set;
        }

        public void Start()
        {
            _logger.Debug("[{0}:{1}] Starting Instance", "Fluint", Id);
            _logger.Debug("[{0}:{1}] OS: Description - {@OSDescription}", "Fluint", Id,
                RuntimeInformation.OSDescription);
            _logger.Debug("[{0}:{1}] OS: Architecture - {@OSArchitecture}", "Fluint", Id,
                RuntimeInformation.OSArchitecture);

            _logger.Debug("[{0}:{1}] .NET: Framework - {@FrameworkDescription}", "Fluint", Id,
                RuntimeInformation.FrameworkDescription);
            _logger.Debug("[{0}:{1}] .NET: Runtime Identifier - {@RuntimeIdentifier}", "Fluint", Id,
                RuntimeInformation.RuntimeIdentifier);
            _logger.Debug("[{0}:{1}] .NET: Process Architecture - {@ProcessArchitecture}", "Fluint", Id,
                RuntimeInformation.ProcessArchitecture);

            TaskManager.Invoke(TaskSchedule.Startup, null);
            TaskManager.Invoke(TaskSchedule.Background, null);
        }

        public void Kill()
        {
            TaskManager.StopAll();
        }
    }
}