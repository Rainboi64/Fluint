// 
// Notification.cs
// 
// Copyright (C) 2023 Yaman Alhalabi

namespace Fluint.Layer.Diagnostics;

public class Notification
{
    public Notification(NotificationType type, string title, string message)
    {
        Message = message;
        Title = title;
        Type = type;
    }

    public NotificationType Type {
        get;
        set;
    }

    public string Message
    {
        get;
    }
    
    public string Title
    {
        get;
    }
}