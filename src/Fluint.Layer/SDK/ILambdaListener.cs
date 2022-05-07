namespace Fluint.Layer.SDK
{
    [Initialization(InitializationMethod.Scoped)]
    public interface ILambdaListener : IModule
    {
        void Execute(string command);
        void Listen();
    }
}