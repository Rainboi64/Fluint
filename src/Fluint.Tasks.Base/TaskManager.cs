//
// TaskManager.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Tasks;

namespace Fluint.Implementation.Tasks
{
    public class TaskManager : ITaskManager
    {
        private readonly ITask[] _backgroundTasks;
        private readonly ModulePacket _packet;

        private readonly ITask[] _startupTasks;
        private readonly IEnumerable<ITask> _tasks;
        private readonly ITask[] _windowDisposingTasks;
        private readonly ITask[] _windowEnterTextTasks;
        private readonly ITask[] _windowReadyTasks;

        private readonly ITask[] _windowRenderTasks;
        private readonly ITask[] _windowResizeTasks;
        private readonly ITask[] _windowScrollTasks;
        private readonly ITask[] _windowUpdateTasks;


        public TaskManager(ModulePacket packet)
        {
            _packet = packet;
            _tasks = _packet.GetInstances().OfType<ITask>();

            // excuse this shit

            _startupTasks = _tasks.Where((x) => x.Schedule == TaskSchedule.Startup).OrderBy(task => task.Priority)
                .ToArray();
            _backgroundTasks = _tasks.Where((x) => x.Schedule == TaskSchedule.Background).OrderBy(task => task.Priority)
                .ToArray();

            _windowRenderTasks = _tasks.Where((x) => x.Schedule == TaskSchedule.WindowRender)
                .OrderBy(task => task.Priority).ToArray();
            _windowUpdateTasks = _tasks.Where((x) => x.Schedule == TaskSchedule.WindowUpdate)
                .OrderBy(task => task.Priority).ToArray();
            _windowResizeTasks = _tasks.Where((x) => x.Schedule == TaskSchedule.WindowResize)
                .OrderBy(task => task.Priority).ToArray();
            _windowEnterTextTasks = _tasks.Where((x) => x.Schedule == TaskSchedule.WindowEnterText)
                .OrderBy(task => task.Priority).ToArray();
            _windowScrollTasks = _tasks.Where((x) => x.Schedule == TaskSchedule.WindowMouseScroll)
                .OrderBy(task => task.Priority).ToArray();
            _windowDisposingTasks = _tasks.Where((x) => x.Schedule == TaskSchedule.WindowDisposing)
                .OrderBy(task => task.Priority).ToArray();

            _windowReadyTasks = _tasks.Where((x) => x.Schedule == TaskSchedule.WindowReady)
                .OrderBy(task => task.Priority).ToArray();
        }

        public void Invoke(TaskSchedule schedule, TaskArgs args)
        {
            switch (schedule)
            {
                case TaskSchedule.Startup:
                    foreach (var item in _startupTasks)
                    {
                        item.Start(args);
                    }

                    break;
                case TaskSchedule.Background:
                    foreach (var item in _backgroundTasks)
                    {
                        new Thread(() => item.Start(args)).Start();
                    }

                    break;

                case TaskSchedule.WindowReady:
                    foreach (var item in _windowReadyTasks)
                    {
                        item.Start(args);
                    }

                    break;
                case TaskSchedule.WindowUpdate:
                    foreach (var item in _windowUpdateTasks)
                    {
                        item.Start(args);
                    }

                    break;
                case TaskSchedule.WindowRender:
                    foreach (var item in _windowRenderTasks)
                    {
                        item.Start(args);
                    }

                    break;
                case TaskSchedule.WindowDisposing:
                    foreach (var item in _windowDisposingTasks)
                    {
                        item.Start(args);
                    }

                    break;
                case TaskSchedule.WindowEnterText:
                    foreach (var item in _windowEnterTextTasks)
                    {
                        item.Start(args);
                    }

                    break;
                case TaskSchedule.WindowMouseScroll:
                    foreach (var item in _windowScrollTasks)
                    {
                        item.Start(args);
                    }

                    break;
                case TaskSchedule.WindowResize:
                    foreach (var item in _windowResizeTasks)
                    {
                        item.Start(args);
                    }

                    break;

                default:
                    throw new NotImplementedException("Couldn't find schedule.");
            }
        }

        public void Invoke(TaskSchedule schedule)
        {
            Invoke(schedule, new TaskArgs(this));
        }
    }
}