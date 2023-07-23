using System.Runtime.InteropServices;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Diagnostics;
using Fluint.Layer.Jobs;

namespace Fluint.Layer.Runtime;

public class Instance : IRuntime
{
    private ILogger _logger;

    public IJobManager JobManager
    {
        get;
        private set;
    }

    public void Create(int id, StartupManifest startupManifest, ModuleManifest moduleManifest, ModulePacket packet,
        InstanceManager parent)
    {
        Packet = packet;
        Parent = parent;
        Id = id;

        StartupManifest = startupManifest;
        ModuleManifest = moduleManifest;

        _logger = Packet.GetSingleton<ILogger>();
        JobManager = Packet.CreateScoped<IJobManager>();
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

        JobManager.Invoke(JobSchedule.Startup, null);
        JobManager.Invoke(JobSchedule.Background, null);
    }

    public void Kill()
    {
        JobManager.StopAll();
    }
}