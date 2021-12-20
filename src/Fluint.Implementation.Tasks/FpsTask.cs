//
// FpsTask.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Tasks;
using Fluint.Layer.UI;
using Fluint.Layer.Windowing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fluint.Implementation.Tasks
{
    public class FpsTask : ITask
    {
        public int Priority => 1;
        public TaskSchedule Schedule => TaskSchedule.WindowUpdate;

        private DateTime _lastTime; // marks the beginning the measurement began
        private int _framesRendered; // an increasing count
        private int _fps; // the FPS calculated from the last measurement

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
