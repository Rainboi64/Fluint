//
// ITaskManager.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

namespace Fluint.Layer.Tasks
{
    [Initialization(InitializationMethod.Scoped)]
    public interface ITaskManager : IModule
    {
        void Invoke(TaskSchedule schedule, TaskArgs args);
        void Invoke(TaskSchedule schedule);
        void StopAll();
    }
}