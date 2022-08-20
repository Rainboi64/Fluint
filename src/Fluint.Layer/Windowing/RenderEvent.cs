// 
// RenderEvent.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using System;

namespace Fluint.Layer.Windowing;

public class RenderEvent : EventArgs
{
    public RenderEvent(double frametime)
    {
        Frametime = frametime;
    }

    public double Frametime
    {
        get;
    }
}