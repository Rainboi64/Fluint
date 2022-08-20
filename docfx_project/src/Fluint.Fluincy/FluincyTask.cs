//
// FluincyTask.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using Fluint.Layer.Configuration;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Tasks;

namespace Fluint.Fluincy
{
    public class FluincyTask : ITask
    {
        private readonly IConfigurationManager _configurationManager;

        public FluincyTask(ModulePacket packet)
        {
            _configurationManager = packet.GetSingleton<IConfigurationManager>();
        }

        public TaskSchedule Schedule => TaskSchedule.Background;
        public int Priority => 1;

        public void Start(TaskArgs args)
        {
        }
    }
}