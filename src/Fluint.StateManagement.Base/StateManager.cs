// 
// StateManager.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using System;
using System.Collections.Generic;
using Fluint.Layer.StateManagement;

namespace Fluint.StateManagement.Base;

public class StateManager : IStateManager
{
    private readonly Dictionary<Type, IState> _states = new();

    public void InitializeState(IState state)
    {
        _states[state.GetType()] = state;
    }

    public T GetState<T>() where T : IState
    {
        return (T)_states[typeof(T)];
    }
}