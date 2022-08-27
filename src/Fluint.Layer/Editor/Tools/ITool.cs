// 
// ITool.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

namespace Fluint.Layer.Editor.Tools;

[Initialization(InitializationMethod.Instanced)]
public interface ITool : IModule
{
    void Begin();
    void Tick();
    void End();
}