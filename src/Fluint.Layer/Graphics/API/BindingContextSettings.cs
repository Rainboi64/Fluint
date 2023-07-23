//
// BindingContextSettings.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;

namespace Fluint.Layer.Graphics.API;

/// <summary>
///     Contains Settings for IBindingContext
/// </summary>
public struct BindingContextSettings
{
    public BindingContextSettings(IntPtr handle, int height, int width)
    {
        ContextHandle = handle;
        Height = height;
        Width = width;
    }

    /// <summary>
    ///     The Control's Context Pointer
    /// </summary>
    public IntPtr ContextHandle
    {
        get;
        set;
    }

    public int Height
    {
        get;
        set;
    }

    public int Width
    {
        get;
        set;
    }
}