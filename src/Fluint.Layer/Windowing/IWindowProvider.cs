﻿//
// IWindowProvider.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;
using System.Collections.Concurrent;
using Fluint.Layer.Mathematics;

namespace Fluint.Layer.Windowing;

[Initialization(InitializationMethod.Scoped)]
public interface IWindowProvider : IModule
{
    // TODO: Flesh this shit out completely.
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

    public ConcurrentQueue<Action> FrameQueue
    {
        get;
    }

    public void SetMouseLocation(Vector2 location);

    public void Adopt<TClient>() where TClient : IWindow;
    public void Start();
    void Close();
}