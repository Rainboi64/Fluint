// 
// IUILayer.cs
// 
// Copyright (C) 2023 Yaman Alhalabi

namespace Fluint.Layer.UI;

public interface IInterfaceLayer
{
    T SpawnControl<T>() where T : Control;

    
}