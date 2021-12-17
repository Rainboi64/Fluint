//
// ITask.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

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
