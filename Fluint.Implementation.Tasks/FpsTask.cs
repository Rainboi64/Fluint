using Fluint.Layer.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fluint.Implementation.Tasks
{
    public class FpsTask : ITask
    {
        public TaskSchedule Schedule => TaskSchedule.PostRender;

        private DateTime _lastTime; // marks the beginning the measurement began
        private int _framesRendered; // an increasing count
        private int _fps; // the FPS calculated from the last measurement

        public void Start()
        {
            _framesRendered++;

            if ((DateTime.Now - _lastTime).TotalSeconds >= 1)
            {
                // one second has elapsed 

                _fps = _framesRendered;
                _framesRendered = 0;
                _lastTime = DateTime.Now;
            }
            Console.Title = $"Fluint FPS: {_fps}";
        }
    }
}
