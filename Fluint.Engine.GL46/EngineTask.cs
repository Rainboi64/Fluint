using Fluint.Layer.Tasks;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Fluint.Engine.GL46
{
    public class EngineTask : ITask
    {
        public TaskSchedule Schedule => TaskSchedule.Startup;

        public void Start()
        {
            Console.WriteLine($"\n\nStarted OpenGL46 Task, from {Assembly.GetExecutingAssembly()}\n\n");
        }
    }
}
