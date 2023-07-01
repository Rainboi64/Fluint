// 
// IMouseCapture.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using Fluint.Layer.Windowing;

namespace Fluint.Layer.Input;

[Initialization(InitializationMethod.Scoped)]
public interface IMouseCapture : IModule
{
    int X
    {
        get;
    }

    int Y
    {
        get;
    }

    void Begin(IWindow window);
    void Capture();
    void Update();
}