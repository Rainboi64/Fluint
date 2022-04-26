//
// FpsTask.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Diagnostics;
using Fluint.Layer.Tasks;
using Fluint.Layer.Windowing;

namespace Fluint.Graphics.Base;

public class FpsTask : ITask
{
    private readonly ModulePacket _packet;

    private int _fps;
    private int _framesRendered;

    private DateTime _lastTime;
    private ILogger _logger;

    public FpsTask(ModulePacket packet)
    {
        _packet = packet;
    }

    public int Priority => 1;
    public TaskSchedule Schedule => TaskSchedule.WindowUpdate;

    public void Start(TaskArgs args)
    {
        _framesRendered++;

        var deltaTime = DateTime.Now - _lastTime;

        if (!(deltaTime.TotalSeconds >= 5))
        {
            return;
        }

        _fps = _framesRendered / 5;
        _framesRendered = 0;
        _lastTime = DateTime.Now;

        // one second has elapsed 
        _logger ??= _packet.GetSingleton<ILogger>();

        var window = args.Invoker as IWindow;
        _logger.Information("[{0}] {1} Average Fps: {2}", "FPSTask", window?.Title, _fps);
    }
}