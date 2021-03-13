using Fluint.Layer.Tasks;
using Fluint.SDK;
using Fluint.Layer.DependencyInjection;
using System;
using System.Reflection;

namespace Fluint.Implementation.Tasks
{
    public class SDKTask : ITask
    {
        public TaskSchedule Schedule => TaskSchedule.Background;
        private readonly ModulePacket _packet;
        public SDKTask(ModulePacket packet)
        {
            _packet = packet;
        }

        public void Start()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Started SDK Console Task, from {Assembly.GetExecutingAssembly()}");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine();

            var sdkBase = new SDKBase(_packet);
            sdkBase.Listen();
        }
    }
}
