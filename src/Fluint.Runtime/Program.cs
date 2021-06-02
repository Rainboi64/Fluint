using System;
using System.IO;
using System.Reflection;
using Fluint.Layer;
using Fluint.Layer.Miscellaneous;
using Fluint.Layer.Tasks;

namespace Fluint.Runtime
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleHelper.WriteEmbeddedColorLine($"\n[red]Welcome to Fluint.[/red]\nStart-line called at {DateTime.Now} Called by {Assembly.GetCallingAssembly()}\nRunning in {Assembly.GetExecutingAssembly()}\n");
         
            var modulesManager = new ModulesManager();
            modulesManager.LoadFolder(@".\modules");
            var collection = modulesManager.ModuleCollection;
            var packet = collection.ModulePacket;

            var taskManager = packet.GetScoped<ITaskManager>();
            taskManager.Invoke(TaskSchedule.Startup, null);
            taskManager.Invoke(TaskSchedule.Background, null);

            
        }
    }
}
