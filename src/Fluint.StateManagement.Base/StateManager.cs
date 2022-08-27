// 
// StateManager.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using System;
using System.Collections.Generic;
using System.Linq;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.StateManagement;

namespace Fluint.StateManagement.Base;

public class StateManager : IStateManager
{
    private readonly Dictionary<IStatefulContext, List<Guid>> _contextGuids = new();
    private readonly ModulePacket _packet;
    private readonly Dictionary<Guid, IState> _states = new();

    public StateManager(ModulePacket packet)
    {
        _packet = packet;
        _contextGuids.Add(new GlobalContext(), new List<Guid>());
    }

    public T InitializeState<T>(IState state) where T : IState
    {
        var guid = Guid.NewGuid();
        _states.Add(guid, state);

        _contextGuids.First().Value.Add(guid);
        return (T)state;
    }

    public T InitializeState<T>(IState state, IStatefulContext context) where T : IState
    {
        var guid = Guid.NewGuid();
        _states.Add(guid, state);

        _contextGuids[context].Add(guid);
        return (T)state;
    }

    public bool HasState<T>()
    {
        var contextMembers = _contextGuids.First().Value;
        return contextMembers.Any(state => _states[state] is T);
    }

    public bool HasState<T>(IStatefulContext context)
    {
        var contextMembers = _contextGuids[context];
        return contextMembers.Any(state => _states[state] is T);
    }

    public T GetState<T>() where T : IState
    {
        var contextMembers = _contextGuids.First().Value;
        foreach (var state in contextMembers)
        {
            if (_states[state] is T)
            {
                return (T)_states[state];
            }
        }

        return InitializeState<T>(_packet.CreateInstance<T>());
    }

    public T GetState<T>(IStatefulContext context) where T : IState
    {
        var contextMembers = _contextGuids[context];
        foreach (var state in contextMembers)
        {
            if (_states[state] is T)
            {
                return (T)_states[state];
            }
        }

        return InitializeState<T>(_packet.CreateInstance<T>(), context);
    }

    public IEnumerable<IState> GetStates(IStatefulContext context)
    {
        var contextMembers = _contextGuids[context];
        foreach (var state in contextMembers)
        {
            yield return _states[state];
        }
    }

    public IStatefulContext InitializeStatefulContext(IStatefulContext context)
    {
        _contextGuids[context] = new List<Guid>(_contextGuids.First().Value);
        return context;
    }

    public IStatefulContext InitializeStatefulContext(IStatefulContext context, IStatefulContext parent)
    {
        _contextGuids[context] = new List<Guid>(_contextGuids[parent]);
        return context;
    }
}