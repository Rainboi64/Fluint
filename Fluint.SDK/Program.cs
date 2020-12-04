using Fluint.Layer;
using Fluint.Layer.Runtime;
using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
namespace Fluint.SDK
{
    class Program
    {
        static void Main(string[] args)
        {
            ModulesManager modulesManager = new ModulesManager();
            modulesManager.LoadFolder("modules");

            var implementationModule = modulesManager.Modules.OfType<ImplementationModule>().FirstOrDefault();
            var parserType = implementationModule.GetImplementations().Where(type => type is ICommandLineArgumentParser).FirstOrDefault();

            var dependencyCollection = new ServiceCollection();
            dependencyCollection.AddScoped<ICommandLineArgumentParser, parserType>();
        }
    }
}
