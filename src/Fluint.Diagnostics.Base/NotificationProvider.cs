// 
// ToastProvider.cs
// 
// Copyright (C) 2023 Yaman Alhalabi

using System.Collections.Generic;
using Fluint.Layer.Diagnostics;

namespace Fluint.Diagnostics.Base;
public class NotificationProvider : INotificationProvider
{
    private readonly List<INotificationReceiver> _toasts = new();
    private readonly List<Notification> _notifications = new();

    public IEnumerable<Notification> Notifications => _notifications;

    public void AddNotification(Notification notification)
    {
        _notifications.Add(notification);
        _toasts.ForEach(toast => toast.AddNotification(notification));
    }

    public void AddNotification(NotificationType type, string title, string message)
    {
        _notifications.Add(new Notification(type, title, message));
        _toasts.ForEach(toast => toast.AddNotification(new Notification(type, title, message)));
    }

    public void Subscribe(INotificationReceiver toast)
    {
        _toasts.Add(toast);
    }

    public void Unsubscribe(INotificationReceiver toast)
    {
        _toasts.Remove(toast);
    }
}