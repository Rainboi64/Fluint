// 
// UIDemoControl.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using System;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Diagnostics;
using Fluint.Layer.Input;
using Fluint.Layer.UI;
using Fluint.Layer.Windowing;

namespace Fluint.Editor.Layout.Base.Controls;

public class UIDemoControl : Control
{
    private readonly Random _random = new();
    private readonly INotificationProvider _notificationProvider;
    private IInputManager _inputManager;

    public UIDemoControl(ModulePacket packet)
    {
        _notificationProvider = packet.GetSingleton<INotificationProvider>();
    }

    public override void Begin(string name, UIContext context)
    {
        _inputManager = context.Window.InputManager;
        base.Begin(name, context);
    }

    public override void Tick()
    {
        if (_inputManager.IsKeyReleased(Key.F6))
        {
            _notificationProvider.AddNotification(
                (NotificationType)_random.Next(0, 5),
                "This is a demo message",
                "this is a test message with a lot of characters as a test, have fun please!");
        }

        base.Tick();
    }
}