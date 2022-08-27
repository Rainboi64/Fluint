// 
// IStateManager.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using System.Collections.Generic;

namespace Fluint.Layer.StateManagement;

[Initialization(InitializationMethod.Singleton)]
public interface IStateManager : IModule
{
    T InitializeState<T>(IState state) where T : IState;
    T InitializeState<T>(IState state, IStatefulContext context) where T : IState;

    bool HasState<T>();
    bool HasState<T>(IStatefulContext context);

    T GetState<T>() where T : IState;
    T GetState<T>(IStatefulContext context) where T : IState;
    IEnumerable<IState> GetStates(IStatefulContext context);

    IStatefulContext InitializeStatefulContext(IStatefulContext context);
    IStatefulContext InitializeStatefulContext(IStatefulContext context, IStatefulContext parent);
}