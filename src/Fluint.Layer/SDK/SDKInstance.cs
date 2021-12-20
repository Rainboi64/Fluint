using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Runtime;

namespace Fluint.Layer.SDK
{
    public class SDKInstance : IRuntime
    {
        public void Create(int id, StartupManifest manifest, ModulePacket packet, InstanceManager parent)
        {
            Packet = packet;
            Parent = parent;
            Id = id;
            Manifest = manifest;
        }

        public int Id { get; private set; }

        public InstanceManager Parent { get; private set; }
        public StartupManifest Manifest { get; private set; }
        public ModulePacket Packet { get; private set; }

        public void Start()
        {
            Packet.CreateScoped<ICommandLineListener>().Listen();
        }

        public void Kill()
        {

        }
    }
}
