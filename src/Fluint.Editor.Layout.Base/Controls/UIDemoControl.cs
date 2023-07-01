// 
// UIDemoControl.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using System;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Input;
using Fluint.Layer.UI;
using Fluint.Layer.Windowing;

namespace Fluint.Editor.Layout.Base.Controls;

public class UIDemoControl : Control
{
    private readonly Random _random = new();
    private readonly IToast _toast;
    private IInputManager _inputManager;

    public UIDemoControl(ModulePacket packet)
    {
        _toast = packet.GetSingleton<IToast>();
        Children.Add(packet.CreateScoped<IDemo>());
    }

    public override void Begin(string name, IWindow parent)
    {
        _inputManager = parent.InputManager;
        base.Begin(name, parent);
    }

    public override void Tick()
    {
        if (_inputManager.IsKeyReleased(Key.F6))
        {
            _toast.AddNotification(
                (NotificationType)_random.Next(0, 5),
                "This is a demo message",
                "this is a test message with a lot of characters as a test, have fun please!");
        }

        base.Tick();
    }
}