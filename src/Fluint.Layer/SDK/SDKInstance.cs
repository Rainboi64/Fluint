using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Runtime;

namespace Fluint.Layer.SDK;

public class SdkInstance : IRuntime
{
    public void Create(int id, StartupManifest startupManifest, ModuleManifest moduleManifest, ModulePacket packet,
        InstanceManager parent)
    {
        Packet = packet;
        Parent = parent;
        Id = id;
        ModuleManifest = moduleManifest;
        StartupManifest = startupManifest;
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

    public StartupManifest StartupManifest
    {
        get;
        private set;
    }

    public ModuleManifest ModuleManifest
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

        // I have no idea why I had this here
        // if (StartupManifest.CommandLineArguments.Length > 1)
        // {
        //     var parser = Packet.CreateScoped<ILambdaParser>();
        //     foreach (var command in StartupManifest.CommandLineArguments)
        //     {
        //         listener.Execute(command);
        //     }
        //
        //     return;
        // }

        listener.Listen();
    }

    public void Kill()
    {
    }
}