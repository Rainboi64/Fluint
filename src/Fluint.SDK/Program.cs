using Fluint.Layer;
using System;

namespace Fluint.SDK
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Fluint SDK";
            Console.WriteLine("Started Fluint SDK.");
            Console.WriteLine("Loading './modules'");
            var packet = ModulesManager.LoadFolder("modules").GenerateModulePacket();
            new SDKBase(packet).Listen();
        }
    }
}
