// 
// IToast.cs
// 
// Copyright (C) 2022 Yaman Alhalabi

namespace Fluint.Layer.UI;

[Initialization(InitializationMethod.Singleton)]
public interface IToast : IGuiComponent, IModule
{
    void AddNotification(NotificationType type, string title, string message);
}