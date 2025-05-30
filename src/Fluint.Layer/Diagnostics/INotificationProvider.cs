// 
// IToastProvider.cs
// 
// Copyright (C) 2023 Yaman Alhalabi

using System.Collections.Generic;
using Fluint.Layer.UI;

namespace Fluint.Layer.Diagnostics;

[Initialization(InitializationMethod.Singleton)]
public interface INotificationProvider : IModule
{
    IEnumerable<Notification> Notifications
    {
        get;
    }

    void AddNotification(Notification notification);
    void AddNotification(NotificationType type, string title, string message);
    void Subscribe(INotificationReceiver subscriber);
    void Unsubscribe(INotificationReceiver subscriber);
}