using Fluint.Engine.GL46.Graphics;
using Fluint.Layer.Configuration;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Miscellaneous;
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
            ConsoleHelper.WriteEmbeddedColorLine($"Started [blue]OpenGL46[/blue] Engine Task");
            // var DebugObject = new Debug();
        }
    }
}
