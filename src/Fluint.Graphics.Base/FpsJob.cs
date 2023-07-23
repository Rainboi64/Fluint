//
// FpsJob.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Diagnostics;
using Fluint.Layer.Jobs;
using Fluint.Layer.Windowing;

namespace Fluint.Graphics.Base;

public class FpsJob : IJob
{
    private readonly ModulePacket _packet;

    private int _fps;
    private int _framesRendered;

    private DateTime _lastTime;
    private ILogger _logger;

    public FpsJob(ModulePacket packet)
    {
        _packet = packet;
    }

    public int Priority => 1;
    public JobSchedule Schedule => JobSchedule.WindowUpdate;

    public void Start(JobArgs args)
    {
        _framesRendered++;

        var deltaTime = DateTime.Now - _lastTime;

        if (!(deltaTime.TotalSeconds >= 1))
        {
            return;
        }

        _fps = _framesRendered / 1;
        _framesRendered = 0;
        _lastTime = DateTime.Now;

        // one second has elapsed 

        _logger ??= _packet.GetSingleton<ILogger>();

        var window = args.Invoker as IWindow;
        window.Title = "Fluint - FPS: " + _fps;
    }
}