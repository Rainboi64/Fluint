using Fluint.Layer;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Fluint.Implementation.Tasks
{
    public class TaskManager : ITaskManager
    {
        private readonly ModulePacket _packet;
        private readonly IEnumerable<ITask> _tasks;

        private readonly ITask[] _startupTasks;
        private readonly ITask[] _readyTasks;
        private readonly ITask[] _preRenderTasks;
        private readonly ITask[] _postRenderTasks;
        private readonly ITask[] _rendererDisposingTasks;
        private readonly ITask[] _backgroundTasks;

        public TaskManager(ModulePacket packet)
        {
            _packet = packet;
            _tasks = _packet.GetInstances().OfType<ITask>();

            _startupTasks = _tasks.Where((x) => x.Schedule == TaskSchedule.Startup).ToArray();
            _backgroundTasks = _tasks.Where((x) => x.Schedule == TaskSchedule.Background).ToArray();
            _readyTasks = _tasks.Where((x) => x.Schedule == TaskSchedule.RendererReady).ToArray();
            _preRenderTasks = _tasks.Where((x) => x.Schedule == TaskSchedule.PreRender).ToArray();
            _postRenderTasks = _tasks.Where((x) => x.Schedule == TaskSchedule.PostRender).ToArray();
            _rendererDisposingTasks = _tasks.Where((x) => x.Schedule == TaskSchedule.RendererDisposing).ToArray();

        }

        public void Invoke(TaskSchedule schedule)
        {
            switch (schedule)
            {
                case TaskSchedule.Startup:
                    foreach (var item in _startupTasks)
                    {
                        item.Start();
                    }
                    break;
                case TaskSchedule.Background:
                    foreach (var item in _backgroundTasks)
                    {
                        new Thread(() => item.Start()).Start();
                    }
                    break;
                case TaskSchedule.PreRender:
                    foreach (var item in _preRenderTasks)
                    {
                        item.Start();
                    }
                    break;
                case TaskSchedule.PostRender:
                    foreach (var item in _postRenderTasks)
                    {
                        item.Start();
                    }
                    break;
                case TaskSchedule.RendererReady:
                    foreach (var item in _readyTasks)
                    {
                        item.Start();
                    }
                    break;
                case TaskSchedule.RendererDisposing:
                    foreach (var item in _rendererDisposingTasks)
                    {
                        item.Start();
                    }
                    break;
                default:
                    throw new NotImplementedException("Couldn't find schedule.");
            }
        }
    }
}
