// 
// IToastProvider.cs
// 
// Copyright (C) 2023 Yaman Alhalabi

using System.Collections.Generic;
using Fluint.Layer.UI;

namespace Fluint.Layer.Diagnostics;

[Initialization(InitializationMethod.Singleton)]
public interface IToastProvider : IModule
{
    IEnumerable<Notification> Notifications
    {
        get;
    }

    void AddNotification(Notification notification);
    void AddNotification(NotificationType type, string title, string message);
    void Subscribe(IToast toast);
    void Unsubscribe(IToast toast);
}