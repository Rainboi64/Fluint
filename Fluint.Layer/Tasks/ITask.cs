using System;
using System.Collections.Generic;
using System.Text;

namespace Fluint.Layer.Tasks
{
    [Initialization(InitializationMethod.Instanced)]
    public interface ITask : IModule
    {
        TaskSchedule Schedule { get; }
        int Priority { get; }
        void Start(TaskArgs args);
    }
}
