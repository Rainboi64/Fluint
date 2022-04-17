//
// EngineTask.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using Fluint.Engine.GL46.Graphics;
using Fluint.Layer.Configuration;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Diagnostics;
using Fluint.Layer.Miscellaneous;
using Fluint.Layer.Tasks;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace Fluint.Engine.GL46
{
    public class EngineTask : ITask
    {
        private static ILogger _logger; 
        public EngineTask(ModulePacket packet)
        {
            _logger = packet.GetSingleton<ILogger>();
        }

        public TaskSchedule Schedule => TaskSchedule.WindowReady;

        public int Priority => 1;

        public void Start(TaskArgs args)
        {
            _logger.Information("Started {0} Engine Task", "OpenGL46");
            _logger.Information("Running OpenGL {0} Shader: {1}", GL.GetString(StringName.Version), GL.GetString(StringName.ShadingLanguageVersion));
            
            GL.DebugMessageCallback(_debugProcCallback, IntPtr.Zero);
            GL.Enable(EnableCap.DebugOutput);
            GL.Enable(EnableCap.DebugOutputSynchronous);
        }


        private static readonly DebugProc _debugProcCallback = DebugCallback;
        private static void DebugCallback(DebugSource source,
            DebugType type,
            int id,
            DebugSeverity severity,
            int length,
            IntPtr message,
            IntPtr userParam)
        {
            var messageString = Marshal.PtrToStringAnsi(message, length);

            if (type == DebugType.DebugTypeError)
            {
                _logger.Error("OGL46 Debugger [{0}]:[{1}]: \n{2}", severity, type, messageString);
            }
            else
            {
               _logger.Information("OGL46 Debugger [{0}]:[{1}]: \n{2}", severity, type, messageString);
            }
        }
    }
}
