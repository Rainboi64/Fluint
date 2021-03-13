using Fluint.Layer.Tasks;
using Fluint.SDK;
using Fluint.Layer.DependencyInjection;
using System;
using System.Reflection;
using Fluint.Layer.Miscellaneous;

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
            ConsoleHelper.WriteEmbeddedColorLine($"Started [green]SDK Console[/green] Task");
            var sdkBase = new SDKBase(_packet);
            sdkBase.Listen();
        }
    }
}
