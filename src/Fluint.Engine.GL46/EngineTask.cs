using Fluint.Engine.GL46.Graphics;
using Fluint.Layer.Configuration;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Miscellaneous;
using Fluint.Layer.Tasks;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Fluint.Engine.GL46
{
    public class EngineTask : ITask
    {
        public TaskSchedule Schedule => TaskSchedule.WindowReady;

        public int Priority => 1;

        public void Start(TaskArgs args)
        {
            ConsoleHelper.WriteEmbeddedColorLine($"Started [blue]OpenGL46[/blue] Engine Task");
            ConsoleHelper.WriteEmbeddedColorLine($"Running [magenta]OpenGL {GL.GetString(StringName.Version)}[/magenta] Shader: [yellow]{GL.GetString(StringName.ShadingLanguageVersion)}[/yellow]");
            Debug.EnableOGLDebugSystem();
        }
    }
}
