//
// IPuppet.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using Fluint.Layer.Mathematics;

namespace Fluint.Layer.Windowing;

public interface IPuppet
{

    public void SetPossessed(IWindow possessor)
    {
    }

    public void OnLoad()
    {
    }

    public void PreRender(double delay)
    {
    }
    
    public void PostRender(double delay)
    {
    }

    public void PreUpdate(double delay)
    {
    }

    public void PostUpdate(double delay)
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