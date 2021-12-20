namespace Fluint.Layer.SDK
{
    [Initialization(InitializationMethod.Scoped)]
    public interface ICommandLineListener : IModule
    {
        void Listen();
    }
}
