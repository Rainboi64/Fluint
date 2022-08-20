// 
// ActionManager.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

namespace Fluint.Layer.Functionality;

[Initialization(InitializationMethod.Scoped)]
public interface IActionManager : IModule
{
    T GetAction<T>() where T : ILogicModule;
}