// 
// IMouseCapture.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using Fluint.Layer.Windowing;

namespace Fluint.Layer.Input;

[Initialization(InitializationMethod.Scoped)]
public interface IMouseCapture : IModule
{
    public int X
    {
        get;
    }

    public int Y
    {
        get;
    }

    public void Begin(IWindow window);
    public void Capture();
    public void Update();
}