using Fluint.Layer;
using Fluint.Layer.Runtime;
using System;
using System.Linq;
using Fluint.Layer.Debugging;
using Fluint.Layer.SDK;
using Fluint.Layer.Configuration;

namespace Fluint.SDK
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Fluint SDK";
            Console.WriteLine("Started Fluint SDK.");
            ModulesManager modulesManager = new ModulesManager();
            Console.WriteLine("Loading './modules'");
            modulesManager.LoadFolder("modules");

            var packet = modulesManager.ModuleCollection.ModulePacket;
            new SDKBase(packet).Listen();
        }
    }
}
