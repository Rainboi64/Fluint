namespace Fluint.Layer.SDK
{
    [Initialization(InitializationMethod.Scoped)]
    public interface ILambdaListener : IModule
    {
        (string command, string[] arguments) Parse(string input);
        void Execute(string command);
        void Listen();
    }
}