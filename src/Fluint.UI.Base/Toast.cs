// 
// Toast.cs
// 
// Copyright (C) 2022 Yaman Alhalabi

using System;
using System.Collections.Generic;
using System.Numerics;
using Fluint.Layer.Configuration;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Diagnostics;
using Fluint.Layer.Localization;
using Fluint.Layer.UI;
using ImGuiNET;

namespace Fluint.UI.Base;

public class 
    Toast : IToast
{
    private const int DefaultLifeCycle = 12;

    private const int XPadding = 20;
    private const int YPadding = 20;
    private const int YMessagePadding = 10;

    private const ImGuiWindowFlags
        NotificationFlags = ImGuiWindowFlags.AlwaysAutoResize | ImGuiWindowFlags.NoDecoration | ImGuiWindowFlags.NoNav |
                            ImGuiWindowFlags.NoInputs | ImGuiWindowFlags.NoSavedSettings;

    private readonly List<ToastNotification> _notifications = new();
    private readonly INotificationProvider _provider;
    private readonly ModulePacket _packet;
    
    private ILocalizationManager _localizationManager;
    private ThemeConfiguration _theme;

    public Toast(ModulePacket packet)
    {
        _packet = packet;
        _provider = _packet.GetSingleton<INotificationProvider>();
        _provider.Subscribe(this);
    }
    
    ~Toast()
    {
        _provider.Unsubscribe(this);
    }
    
    public string Name
    {
        get;
        private set;
    }

    public void Begin(string name)
    {
        Name = name;
        _theme = _packet.GetSingleton<IConfigurationManager>().Get<ThemeConfiguration>();
        _localizationManager = _packet.GetSingleton<ILocalizationManager>();
    }

    public void Tick()
    {
        var purgeList = new List<ToastNotification>();

        var size = ImGui.GetMainViewport().Size;

        var io = ImGui.GetIO();
        var deltaTime = io.DeltaTime;
        var height = 0.0f;

        foreach (var notification in _notifications)
        {
            notification.LifeCycle -= deltaTime;
            var opacity = notification.LifeCycle / DefaultLifeCycle;

            var open = true;
            if (opacity <= 0.0f)
            {
                purgeList.Add(notification);
                open = false;
            }

            var color = notification.Type switch
            {
                NotificationType.Success => _theme.Success,
                NotificationType.Error => _theme.Error,
                NotificationType.Warning => _theme.Warning,
                NotificationType.Information => _theme.Information,
                NotificationType.Debug => _theme.Debug,
                _ => throw new ArgumentOutOfRangeException()
            };

            ImGui.SetNextWindowBgAlpha(opacity);
            ImGui.SetNextWindowPos(new Vector2(size.X - XPadding, size.Y - YPadding - height), ImGuiCond.Always,
                new Vector2(1, 1));

            ImGui.Begin(notification.Title, ref open, NotificationFlags);
            {
                ImGui.PushTextWrapPos(size.X / 3f);
                ImGui.TextColored(color with { W = opacity },
                    $"{_localizationManager.Fetch(notification.Type.ToString())}: {notification.Title}");
                ImGui.Separator();
                ImGui.TextColored(_theme.Text with { W = opacity }, notification.Message);
                ImGui.PopTextWrapPos();
            }
            ImGui.End();

            height += ImGui.GetWindowHeight() + YMessagePadding;
        }

        foreach (var item in purgeList)
        {
            _notifications.Remove(item);
        }
    }

    public void AddNotification(Notification notification)
    {
        _notifications.Add(new ToastNotification(notification.Type, notification.Title, notification.Message));
    }
    
    private class ToastNotification : Notification
    {
        public ToastNotification(NotificationType type, string title, string message) : base(type, title, message)
        {
            LifeCycle = DefaultLifeCycle;
        }
        
        public float LifeCycle
        {
            get;
            set;
        }
    }
}