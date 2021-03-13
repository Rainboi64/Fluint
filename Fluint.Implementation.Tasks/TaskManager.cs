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

        private readonly IEnumerable<ITask> _startupTasks;
        private readonly IEnumerable<ITask> _updateTasks;
        private readonly IEnumerable<ITask> _backgroundTasks;

        public TaskManager(ModulePacket packet)
        {
            _packet = packet;
            _tasks = _packet.GetInstances().OfType<ITask>();

            _startupTasks = _tasks.Where((x) => x.Schedule == TaskSchedule.Startup);
            _updateTasks = _tasks.Where((x) => x.Schedule == TaskSchedule.Update);
            _backgroundTasks = _tasks.Where((x) => x.Schedule == TaskSchedule.Background);

        }

        public void Invoke(TaskSchedule schedule)
        {
            switch (schedule)
            {
                case TaskSchedule.Startup:
                    foreach (var item in _startupTasks) item.Start();
                    break;
                case TaskSchedule.Background:
                    foreach (var item in _backgroundTasks) new Thread(() => item.Start()).Start();
                    break;
                case TaskSchedule.Update:
                    foreach (var item in _updateTasks) item.Start();
                    break;
                default:
                    throw new NotImplementedException("Couldn't find schedule.");
            }
        }
    }
}
