// 
// IStateManager.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

namespace Fluint.Layer.StateManagement;

[Initialization(InitializationMethod.Singleton)]
public interface IStateManager : IModule
{
    void InitializeState(IState state);
    T GetState<T>() where T : IState;
}