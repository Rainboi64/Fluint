// 
// Notification.cs
// 
// Copyright (C) 2023 Yaman Alhalabi

using Fluint.Layer.Diagnostics;
using Fluint.Layer.UI;

namespace Fluint.Editor.Layout.Base.Controls;

public class NotificationItem : INotificationItem
{
    public string Name
    {
        get;
    }

    public void Begin(string name)
    {
        name = Name;
    }

    public void Tick()
    {
        
    }

    public Notification Info
    {
        get;
        set;
    }
}