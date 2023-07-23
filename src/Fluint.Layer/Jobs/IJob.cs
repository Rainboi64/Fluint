//
// IJob.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

namespace Fluint.Layer.Jobs;

[Initialization(InitializationMethod.Instanced)]
public interface IJob : IModule
{
    JobSchedule Schedule
    {
        get;
    }

    int Priority
    {
        get;
    }

    void Start(JobArgs args);
}