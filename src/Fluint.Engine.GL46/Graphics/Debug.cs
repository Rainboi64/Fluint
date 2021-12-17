//
// Debug.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;
using System.Runtime.InteropServices;
using OpenTK.Graphics.OpenGL4;
using Fluint.Layer.Miscellaneous;

namespace Fluint.Engine.GL46.Graphics
{
    public class Debug
    {
        public static void EnableOGLDebugSystem()
        {
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
                ConsoleHelper.WriteEmbeddedColorLine($"[red]OGL46[/red]:[green]{severity}[/green]|[yellow]{type}[/yellow]:\n{messageString}");
            }
            else
            {
                ConsoleHelper.WriteEmbeddedColorLine($"[red]OGL46[/red]:[green]{severity}[/green]|[yellow]{type}[/yellow]:\n{messageString}");
            }
        }
    }
}
