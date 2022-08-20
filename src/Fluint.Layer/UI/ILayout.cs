// 
// ILayout.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using Fluint.Layer.Windowing;

namespace Fluint.Layer.UI;

[Initialization(InitializationMethod.Instanced)]
public interface ILayout : IModule
{
    void Initialize(IWindow window);
}