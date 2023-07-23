//
// IPuppet.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using Fluint.Layer.Mathematics;

namespace Fluint.Layer.Windowing;

public interface IPuppet
{
    /// <summary>
    ///     Don't call outside IWindow
    /// </summary>
    public void SetPossessed(in IWindow possessor)
    {
    }

    public void OnLoad()
    {
    }

    public void OnRender(double delay)
    {
    }

    public void OnUpdate(double delay)
    {
    }

    public void OnMouseWheelMoved(Vector2 offset)
    {
    }

    public void OnStart()
    {
    }

    public void OnTextReceived(int unicode, string data)
    {
    }

    public void OnResize(int width, int height)
    {
    }
}