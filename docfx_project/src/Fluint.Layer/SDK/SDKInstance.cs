using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Runtime;

namespace Fluint.Layer.SDK
{
    public class SdkInstance : IRuntime
    {
        public void Create(int id, StartupManifest manifest, ModulePacket packet, InstanceManager parent)
        {
            Packet = packet;
            Parent = parent;
            Id = id;
            Manifest = manifest;
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
            var listener = Packet.CreateScoped<ILambdaListener>();

            if (Manifest.CommandLineArguments.Length > 1)
            {
                var parser = Packet.CreateScoped<ILambdaParser>();
                foreach (var command in Manifest.CommandLineArguments)
                {
                    listener.Execute(command);
                }

                return;
            }

            listener.Listen();
        }

        public void Kill()
        {
        }
    }
}