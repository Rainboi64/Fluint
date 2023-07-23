//
// JobManager.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Jobs;

namespace Fluint.Tasks.Base
{
    public class JobManager : IJobManager
    {
        private readonly IJob[] _backgroundTasks;

        private readonly List<Task> _runningBackgroundTasks = new();

        private readonly IJob[] _startupTasks;
        private readonly IJob[] _windowDisposingTasks;
        private readonly IJob[] _windowEnterTextTasks;
        private readonly IJob[] _windowReadyTasks;

        private readonly IJob[] _windowRenderTasks;
        private readonly IJob[] _windowResizeTasks;
        private readonly IJob[] _windowScrollTasks;
        private readonly IJob[] _windowUpdateTasks;

        public JobManager(ModulePacket packet)
        {
            var jobs = packet.GetInstances().OfType<IJob>();

            var enumerable = jobs as IJob[] ?? jobs.ToArray();
            _startupTasks = enumerable.Where(x => x.Schedule == JobSchedule.Startup).OrderBy(task => task.Priority)
                .ToArray();
            _backgroundTasks = enumerable.Where(x => x.Schedule == JobSchedule.Background)
                .OrderBy(task => task.Priority)
                .ToArray();

            _windowRenderTasks = enumerable.Where(x => x.Schedule == JobSchedule.WindowRender)
                .OrderBy(task => task.Priority).ToArray();
            _windowUpdateTasks = enumerable.Where(x => x.Schedule == JobSchedule.WindowUpdate)
                .OrderBy(task => task.Priority).ToArray();
            _windowResizeTasks = enumerable.Where(x => x.Schedule == JobSchedule.WindowResize)
                .OrderBy(task => task.Priority).ToArray();
            _windowEnterTextTasks = enumerable.Where(x => x.Schedule == JobSchedule.WindowEnterText)
                .OrderBy(task => task.Priority).ToArray();
            _windowScrollTasks = enumerable.Where(x => x.Schedule == JobSchedule.WindowMouseScroll)
                .OrderBy(task => task.Priority).ToArray();
            _windowDisposingTasks = enumerable.Where(x => x.Schedule == JobSchedule.WindowDisposing)
                .OrderBy(task => task.Priority).ToArray();

            _windowReadyTasks = enumerable.Where(x => x.Schedule == JobSchedule.WindowReady)
                .OrderBy(task => task.Priority).ToArray();
        }

        public void Invoke(JobSchedule schedule, JobArgs args)
        {
            switch (schedule)
            {
                case JobSchedule.Startup:
                    foreach (var item in _startupTasks)
                    {
                        item.Start(args);
                    }

                    break;
                case JobSchedule.Background:
                    foreach (var item in _backgroundTasks)
                    {
                        _runningBackgroundTasks.Add(
                            Task.Run(() => {
                                item.Start(args);
                            }));
                    }

                    break;

                case JobSchedule.WindowReady:
                    foreach (var item in _windowReadyTasks)
                    {
                        item.Start(args);
                    }

                    break;
                case JobSchedule.WindowUpdate:
                    foreach (var item in _windowUpdateTasks)
                    {
                        item.Start(args);
                    }

                    break;
                case JobSchedule.WindowRender:
                    foreach (var item in _windowRenderTasks)
                    {
                        item.Start(args);
                    }

                    break;
                case JobSchedule.WindowDisposing:
                    foreach (var item in _windowDisposingTasks)
                    {
                        item.Start(args);
                    }

                    break;
                case JobSchedule.WindowEnterText:
                    foreach (var item in _windowEnterTextTasks)
                    {
                        item.Start(args);
                    }

                    break;
                case JobSchedule.WindowMouseScroll:
                    foreach (var item in _windowScrollTasks)
                    {
                        item.Start(args);
                    }

                    break;
                case JobSchedule.WindowResize:
                    foreach (var item in _windowResizeTasks)
                    {
                        item.Start(args);
                    }

                    break;

                default:
                    throw new NotImplementedException("Couldn't find schedule.");
            }
        }

        public void Invoke(JobSchedule schedule)
        {
            Invoke(schedule, new JobArgs(this));
        }

        public void StopAll()
        {
            foreach (var task in _runningBackgroundTasks)
            {
                task.Dispose();
            }
        }
    }
}