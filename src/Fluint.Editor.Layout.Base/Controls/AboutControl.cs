// 
// AboutControl.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Localization;
using Fluint.Layer.Mathematics;
using Fluint.Layer.UI;

namespace Fluint.Editor.Layout.Base.Controls;

public class AboutControl : Control
{
    public AboutControl(ModulePacket packet)
    {
        var localizationManager = packet.GetSingleton<ILocalizationManager>();
        var container = packet.CreateScoped<IContainer>();

        container.Title = localizationManager.Fetch("about");

        var logo = packet.CreateScoped<IImage>();
        logo.Path = "./assets/full.png";
        logo.Size = new Vector2i(250, 250);

        var versionLabel = packet.CreateScoped<ITextLabel>();
        versionLabel.Text = packet.CurrentRuntime.StartupManifest.VersionDetails;

        var copyrightLabel = packet.CreateScoped<ITextLabel>();
        copyrightLabel.Text = localizationManager.Fetch("copyright_disclosure");

        var creatorLabel = packet.CreateScoped<ITextLabel>();
        creatorLabel.Text = localizationManager.Fetch("creator_info");

        container.Add("Logo", logo);
        container.Add("Version Label", versionLabel);
        container.Add("Copyright Label", copyrightLabel);
        container.Add("Creator Label", creatorLabel);
        Children.Add(container);
    }
}