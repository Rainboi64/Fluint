// 
// NotificationsWindow.cs
// 
// Copyright (C) 2023 Yaman Alhalabi

using System;
using System.Collections.Generic;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Diagnostics;
using Fluint.Layer.UI;
using Fluint.Layer.Windowing;

namespace Fluint.Editor.Layout.Base.Controls;

public class NotificationsControl : Control, INotificationReceiver
{
    private readonly List<Notification> _notifications = new();
    private readonly IContainer _container;
    private readonly ModulePacket _packet;
    private UIContext _uiContext;
    
    public NotificationsControl(ModulePacket packet)
    {
        _packet = packet;
        var provider = packet.GetSingleton<INotificationProvider>();
        provider.Subscribe(this);
        _notifications.AddRange(provider.Notifications);

        _container = packet.CreateScoped<IContainer>();

        foreach (var notification in _notifications)
        {
            var notificationItem = _packet.CreateScoped<INotificationItem>();
            notificationItem.Info = notification;
        
            _container.Add($"{notification.Title}: {notification.Message} : {DateTime.Now.Ticks}", notificationItem);
        }
        
        Children.Add(_container);
    }

    public override void Begin(string name, UIContext context)
    {
        _uiContext = context;
        base.Begin(name, context);
    }
    
    public void AddNotification(Notification notification)
    {
        _uiContext.Enqueue(() =>
        {
            _notifications.Add(notification);

            var notificationItem = _packet.CreateScoped<INotificationItem>();
            notificationItem.Info = notification;
            
            _container.Add($"{notification.Title}: {notification.Message} : {DateTime.Now.Ticks}", notificationItem);
        });
    }
}