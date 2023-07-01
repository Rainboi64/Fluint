// 
// ITool.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using Fluint.Layer.Editor.Viewport;

namespace Fluint.Layer.Editor.Tools;

[Initialization(InitializationMethod.Instanced)]
public interface ITool : IModule
{
    void Begin(IViewportContext context);
}