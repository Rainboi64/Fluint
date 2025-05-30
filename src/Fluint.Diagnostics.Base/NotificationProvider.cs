// 
// ToastProvider.cs
// 
// Copyright (C) 2023 Yaman Alhalabi

using System.Collections.Generic;
using Fluint.Layer.Diagnostics;
using Fluint.Layer.UI;

namespace Fluint.Diagnostics.Base;
public class ToastProvider : IToastProvider
{
    private readonly List<IToast> _toasts = new();
    public void AddNotification(NotificationType type, string title, string message)
    {
        _toasts.ForEach(toast => toast.AddNotification(type, title, message));
    }

    public void Subscribe(IToast toast)
    {
        _toasts.Add(toast);
    }

    public void Unsubscribe(IToast toast)
    {
        _toasts.Remove(toast);
    }
}