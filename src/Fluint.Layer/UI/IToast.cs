// 
// IToast.cs
// 
// Copyright (C) 2022 Yaman Alhalabi

using Fluint.Layer.Diagnostics;

namespace Fluint.Layer.UI;

[Initialization(InitializationMethod.Scoped)]
public interface IToast : IGuiComponent, INotificationReceiver, IModule
{
    
}