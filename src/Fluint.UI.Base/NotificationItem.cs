// 
// Notification.cs
// 
// Copyright (C) 2023 Yaman Alhalabi

using System;
using System.Numerics;
using Fluint.Layer.Configuration;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Diagnostics;
using Fluint.Layer.Localization;
using Fluint.Layer.UI;
using ImGuiNET;

namespace Fluint.UI.Base;

public class NotificationItem : INotificationItem
{
    // private const int XPadding = 20;
    // private const int YPadding = 20;
    // private const int YMessagePadding = 10;

    private const ImGuiWindowFlags
        NotificationFlags =  ImGuiWindowFlags.NoNav |
                            ImGuiWindowFlags.NoInputs | ImGuiWindowFlags.NoSavedSettings;

    private readonly ILocalizationManager _localizationManager;
    private readonly ThemeConfiguration _theme;

    public NotificationItem(ModulePacket packet)
    {
        _theme = packet.GetSingleton<IConfigurationManager>().Get<ThemeConfiguration>();
        _localizationManager = packet.GetSingleton<ILocalizationManager>();
    }
    
    public string Name
    {
        get;
        private set;
    }

    public void Begin(string name)
    { 
        Name = name;
    }

    public void Tick()
    {

        var color = Info.Type switch
        {
            NotificationType.Success => _theme.Success,
            NotificationType.Error => _theme.Error,
            NotificationType.Warning => _theme.Warning,
            NotificationType.Information => _theme.Information,
            NotificationType.Debug => _theme.Debug,
            _ => throw new ArgumentOutOfRangeException()
        };
        
        var size = ImGui.GetWindowSize();
        
        

        ImGui.BeginChild(Info.Title);
        {
            ImGui.PushTextWrapPos(size.X / 3f);
            ImGui.TextColored(color,
                $"{_localizationManager.Fetch(Info.Type.ToString())}: {Info.Title}");
            ImGui.Separator();
            ImGui.TextColored(_theme.Text, Info.Message);
            ImGui.PopTextWrapPos();
        }
        ImGui.EndChild();
        
    }

    public Notification Info
    {
        get;
        set;
    }
}