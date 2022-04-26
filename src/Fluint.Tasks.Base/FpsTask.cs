//
// FpsTask.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;
using Fluint.Layer.Tasks;
using Fluint.Layer.Windowing;

namespace Fluint.Implementation.Tasks
{
    public class FpsTask : ITask
    {
        private int _fps; // the FPS calculated from the last measurement
        private int _framesRendered; // an increasing count

        private DateTime _lastTime; // marks the beginning the measurement began
        public int Priority => 1;
        public TaskSchedule Schedule => TaskSchedule.WindowUpdate;

        public void Start(TaskArgs args)
        {
            var window = args.Invoker as IWindow;
            _framesRendered++;

            if ((DateTime.Now - _lastTime).TotalSeconds >= 1)
            {
                // one second has elapsed 

                _fps = _framesRendered;
                _framesRendered = 0;
                _lastTime = DateTime.Now;

                window.Title = $"Fluint FPS: {_fps}";
            }
        }
    }
}