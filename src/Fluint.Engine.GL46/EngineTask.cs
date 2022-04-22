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

            _logger.Information("[{0}] OpenGL Version: {1}", "OpenGL46", GL.GetString(StringName.Version));
            _logger.Information("[{0}] Version: {1}", "OpenGL46", GL.GetString(StringName.ShadingLanguageVersion));
            _logger.Information("[{0}] Renderer: {1}", "OpenGL46", GL.GetString(StringName.Renderer));
            _logger.Information("[{0}] Vendor: {1}", "OpenGL46", GL.GetString(StringName.Vendor));


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
                _logger.Error("[{0}] Debugger [{1}]:[{2}]: {3}", "OpenGL46", severity, type, messageString);
            }
            else
            {
               _logger.Information("[{0}] Debugger [{1}]:[{2}]: {3}", "OpenGL46", severity, type, messageString);
            }
        }
    }
}
