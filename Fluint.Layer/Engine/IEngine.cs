namespace Fluint.Layer.Engine
{
    [Initialization(InitializationMethod.Scoped)]
    public interface IEngine : IModule
    {
        void Start(EngineMode mode, string[] arguments);
        void Stop();
    }
}
