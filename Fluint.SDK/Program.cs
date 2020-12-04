using Fluint.Layer;
using Fluint.Layer.Runtime;
using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Fluint.Layer.Debugging;
using Fluint.Layer.SDK;

namespace Fluint.SDK
{
    class Program
    {
        static void Main(string[] args)
        {
            ModulesManager modulesManager = new ModulesManager();
            modulesManager.LoadFolder("modules");

            var packet = modulesManager.ModuleCollection.ModulePacket;
            var logger = packet.GetSingleton<ILogger>();
            var parser = packet.GetScoped<IParser>();
            parser.Parse("hello",null);
            logger.Error("We did it reddit!");
            Console.ReadLine();
        }
    }
}
