//
// SDKTask.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using Fluint.Layer.Tasks;
using Fluint.SDK;
using Fluint.Layer.DependencyInjection;
using System;
using System.Reflection;
using Fluint.Layer.Miscellaneous;

namespace Fluint.Implementation.Tasks
{
    public class SDKTask : ITask
    {
        public TaskSchedule Schedule => TaskSchedule.Background;

        public int Priority => 0;

        private readonly ModulePacket _packet;
        public SDKTask(ModulePacket packet)
        {
            _packet = packet;
        }

        public void Start(TaskArgs args)
        {
            ConsoleHelper.WriteEmbeddedColorLine($"Started [green]SDK Console[/green] Task");
            var sdkBase = new SDKBase(_packet);
            sdkBase.Listen();
        }
    }
}
