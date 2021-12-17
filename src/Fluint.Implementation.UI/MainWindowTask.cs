//
// MainWindowTask.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Tasks;
using Fluint.Layer.Windowing;

namespace Fluint.Implementation.UI
{
    public class MainWindowTask : ITask
    {
        public int Priority => 1;

        private readonly ModulePacket _packet;
        public MainWindowTask(ModulePacket packet)
        {
            _packet = packet;
        }

        public TaskSchedule Schedule => TaskSchedule.Background;

        public void Start(TaskArgs args)
        {
            var provider = _packet.CreateScoped<IWindowProvider>();
            provider.Adopt<MainWindow>();
            provider.Start();
        }
    }
}
