// 
// INotificationReceiver.cs
// 
// Copyright (C) 2023 Yaman Alhalabi

namespace Fluint.Layer.Diagnostics;

public interface INotificationReceiver
{
    public void AddNotification(Notification notification);
}