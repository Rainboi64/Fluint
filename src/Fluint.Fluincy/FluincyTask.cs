//
// FluincyTask.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fluint.Implementation.Configuration;
using Fluint.Layer.Configuration;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Tasks;

namespace Fluint.Fluincy
{
    public class FluincyTask : ITask
    {
        public TaskSchedule Schedule => TaskSchedule.Background;
        public int Priority => 1;

        private readonly IConfigurationManager _configurationManager;

        public FluincyTask(ModulePacket packet)
        {
            _configurationManager = packet.GetSingleton<IConfigurationManager>();
        }

        public void Start(TaskArgs args)
        {
            if (!_configurationManager.Contains<EnableFluincyConfiguration>())
            {
                //_configurationManager.Add(new EnableFluincyConfiguration() { EnableFluincyServices })
            }
        }
    }
}
