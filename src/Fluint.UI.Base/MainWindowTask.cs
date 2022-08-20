//
// MainWindowTask.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using Fluint.Layer.Configuration;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Localization;
using Fluint.Layer.Tasks;
using Fluint.Layer.UI;
using Fluint.Layer.Windowing;

namespace Fluint.UI.Base
{
    public class MainWindowTask : ITask
    {
        private readonly ModulePacket _packet;

        public MainWindowTask(ModulePacket packet)
        {
            _packet = packet;
        }

        public int Priority => 1;

        public TaskSchedule Schedule => TaskSchedule.Startup;

        public void Start(TaskArgs args)
        {
            var configurationManager = _packet.GetSingleton<IConfigurationManager>();
            var uiConfiguration = configurationManager.Get<UIConfiguration>();

            _packet.GetSingleton<ILocalizationManager>().ActiveLanguage = uiConfiguration.Language;
            
            var provider = _packet.CreateScoped<IWindowProvider>();
            
            provider.Adopt<MainWindow>();
            provider.Start();
        }
    }
}