using Fluint.Layer;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Diagnostics;
using Fluint.Layer.Graphics;
using Fluint.Layer.SDK;
using System;
using System.Linq;
using System.Threading;

namespace APITest
{
    class Program
    {
        static void Main(string[] args)
        {
            ModulesManager modulesManager = new ModulesManager();
            modulesManager.LoadFolder("./modules");
            var pack = modulesManager.ModuleCollection.ModulePacket;

            var benchmark = new Benchmark(() => 
            {
                var xd = pack.New<IShader>();
            }, 1000000);

            benchmark.Start();
            Console.WriteLine(benchmark);

            while (true) Console.ReadLine();
        }
    }
}
