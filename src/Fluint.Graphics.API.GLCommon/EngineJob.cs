﻿//
// EngineJob.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;
using System.Runtime.InteropServices;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Diagnostics;
using Fluint.Layer.Jobs;
using OpenTK.Graphics.OpenGL4;

namespace Fluint.Graphics.API.GLCommon;

public class EngineJob : IJob
{
    private static ILogger _logger;


    private static readonly DebugProc DebugProcCallback = DebugCallback;

    public EngineJob(ModulePacket packet)
    {
        _logger = packet.GetSingleton<ILogger>();
    }

    public JobSchedule Schedule => JobSchedule.WindowReady;

    public int Priority => 1;

    public void Start(JobArgs args)
    {
        _logger.Debug("[{0}] OpenGL Version: {1}", "OpenGL46", GL.GetString(StringName.Version));
        _logger.Debug("[{0}] Version: {1}", "OpenGL46", GL.GetString(StringName.ShadingLanguageVersion));
        _logger.Debug("[{0}] Renderer: {1}", "OpenGL46", GL.GetString(StringName.Renderer));
        _logger.Debug("[{0}] Vendor: {1}", "OpenGL46", GL.GetString(StringName.Vendor));
        _logger.Debug("[{0}] Extensions: {1}", "OpenGL46", GL.GetString(StringName.Extensions));

        GL.DebugMessageCallback(DebugProcCallback, IntPtr.Zero);
        GL.Enable(EnableCap.DebugOutput);
        GL.Enable(EnableCap.DebugOutputSynchronous);
    }

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
            throw new EngineApiException("OpenGL46", messageString);
        }

        _logger.Debug("[{0}] Debugger [{1}]:[{2}]: {3}", "OpenGL46", severity, type, messageString);
    }
}