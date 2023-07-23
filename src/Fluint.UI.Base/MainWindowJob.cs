//
// MainWindowJob.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using Fluint.Layer.Configuration;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Jobs;
using Fluint.Layer.Localization;
using Fluint.Layer.UI;
using Fluint.Layer.Windowing;

namespace Fluint.UI.Base;

public class MainWindowJob : IJob
{
    private readonly ModulePacket _packet;

    public MainWindowJob(ModulePacket packet)
    {
        _packet = packet;
    }

    public int Priority => 1;

    public JobSchedule Schedule => JobSchedule.Startup;

    public void Start(JobArgs args)
    {
        var configurationManager = _packet.GetSingleton<IConfigurationManager>();
        var uiConfiguration = configurationManager.Get<UIConfiguration>();

        _packet.GetSingleton<ILocalizationManager>().ActiveLanguage = uiConfiguration.Language;

        var provider = _packet.CreateScoped<IWindowProvider>();

        provider.Adopt<MainWindow>();
        provider.Start();
    }
}