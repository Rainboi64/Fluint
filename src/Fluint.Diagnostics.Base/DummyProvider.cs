// 
// DummyProvider.cs
// 
// Copyright (C) 2023 Yaman Alhalabi

using System.Collections.Generic;
using System.Linq;
using Fluint.Layer.Diagnostics;
using Fluint.Layer.UI;

namespace Fluint.Diagnostics.Base;

internal class DummyProvider : INotificationProvider
{
    public IEnumerable<Notification> Notifications
    {
        get;
    } = Enumerable.Empty<Notification>();

    public void AddNotification(Notification notification)
    {
    }

    public void AddNotification(NotificationType type, string title, string message)
    {
    }

    public void Subscribe(INotificationReceiver subscriber)
    {
    }

    public void Unsubscribe(INotificationReceiver subscriber)
    {
    }
}