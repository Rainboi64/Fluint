//
// IWindowProvider.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;
using System.Collections.Concurrent;
using Fluint.Layer.Mathematics;
using Fluint.Layer.Windowing;

namespace Fluint.Layer.Graphics.API;

[Initialization(InitializationMethod.Scoped)]
public interface IWindowProvider : IModule
{
    IWindow Client
    {
        get;
    }

    public object NativeKeyboardObject
    {
        get;
    }

    public object NativeMouseObject
    {
        get;
    }

    public string WindowTitle
    {
        get;
        set;
    }

    public bool WindowVSync
    {
        get;
        set;
    }

    public Vector2i WindowSize
    {
        get;
        set;
    }

    public Vector2i WindowLocation
    {
        get;
        set;
    }

    public Vector2i ScreenSize
    {
        get;
    }

    public Vector2 ScaleRate
    {
        get;
    }

    public ConcurrentQueue<Action> FrameQueue
    {
        get;
    }

    public void SetMouseLocation(Vector2 location);
    public void Adopt(IWindow window);
    public void Start();
    void Close();
}