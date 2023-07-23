//
// IJobManager.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

namespace Fluint.Layer.Jobs;

[Initialization(InitializationMethod.Scoped)]
public interface IJobManager : IModule
{
    void Invoke(JobSchedule schedule, JobArgs args);
    void Invoke(JobSchedule schedule);
    void StopAll();
}