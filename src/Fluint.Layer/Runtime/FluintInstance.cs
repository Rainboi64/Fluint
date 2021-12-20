using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Tasks;

namespace Fluint.Layer.Runtime
{
    public class FluintInstance : IRuntime
    {
        public void Create(int id, StartupManifest manifest, ModulePacket packet, InstanceManager parent)
        {
            Packet = packet;
            Parent = parent;
            Id = id;
            Manifest = manifest;

            TaskManager = Packet.CreateScoped<ITaskManager>();
        }

        public int Id { get; private set; }

        public InstanceManager Parent { get; private set; }
        public StartupManifest Manifest { get; private set; }
        public ModulePacket Packet { get; private set; }

        public ITaskManager TaskManager { get; private set; }

        public void Start()
        {
            TaskManager.Invoke(TaskSchedule.Startup, null);
            TaskManager.Invoke(TaskSchedule.Background, null);
        }

        public void Kill()
        {

        }
    }
}
