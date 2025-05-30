// 
// INotificationItem.cs
// 
// Copyright (C) 2023 Yaman Alhalabi

using Fluint.Layer.Diagnostics;

namespace Fluint.Layer.UI;

[Initialization(InitializationMethod.Scoped)]
public interface INotificationItem : IGuiComponent, IModule
{
    Notification Info
    {
        get;
        set;
    }
}