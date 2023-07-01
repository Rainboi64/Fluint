// 
// TickEventArgs.cs
// 
// Copyright (C) 2022 Yaman Alhalabi

using System;
using Fluint.Layer.StateManagement;

namespace Fluint.Layer.Editor.Viewport;

public class TickEventArgs : EventArgs
{
    public IStatefulContext Context;

    public TickEventArgs(double time)
    {
        Time = time;
    }

    public double Time
    {
        get;
    }
}