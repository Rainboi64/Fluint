using System;
using System.Collections.Generic;
using System.Text;

namespace Fluint.Layer.Tasks
{
    [Initialization(InitializationMethod.Scoped)]
    public interface ITaskManager : IModule
    {
        void Invoke(TaskSchedule schedule, TaskArgs args);
        void Invoke(TaskSchedule schedule);
    }
}
