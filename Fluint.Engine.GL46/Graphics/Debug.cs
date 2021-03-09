//
// Debug.cs
//
// Copyright (C) 2020 Yaman Alhalabi
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;
using Fluint.Layer.Diagnostics;

namespace Fluint.Engine.GL46.Graphics
{
    public class Debug
    {
        //private static ILogger _logger;

        /// <summary>
        /// Enables The call-back of GL errors to be displayed in Console.
        /// </summary>
        public Debug()
        {
            _debugProcCallbackHandle = GCHandle.Alloc(_debugProcCallback);

            GL.DebugMessageCallback(_debugProcCallback, IntPtr.Zero);
            GL.Enable(EnableCap.DebugOutput);
            GL.Enable(EnableCap.DebugOutputSynchronous);
        }

        private static DebugProc _debugProcCallback = DebugCallback;
        private static GCHandle _debugProcCallbackHandle;
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
                Console.WriteLine($"OpenGL Debug System: {severity} {type} | {messageString}");
            else
                Console.WriteLine($"OpenGL Debug System: {severity} {type} | {messageString}");
        }
    }
}
