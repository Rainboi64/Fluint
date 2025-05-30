// 
// UIContext.cs
// 
// Copyright (C) 2023 Yaman Alhalabi

using System;
using Fluint.Layer.Windowing;

namespace Fluint.Layer.UI;

public class UIContext
{
    public UIContext(IWindow window, IUILayer layer)
    {
        Window = window;
        UILayer = layer;
    }
    
    public IWindow Window  
    {
        get;
    }

    public IUILayer UILayer
    {
        get;
    }

    public void Enqueue(Action action)
    {
        Window.Enqueue(action);
    }
}