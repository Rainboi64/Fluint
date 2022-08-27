// 
// ReplicateAttribute.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using System;

namespace Fluint.Layer.StateManagement;

[AttributeUsage(AttributeTargets.Property)]
public class Replicable : Attribute
{
    public ReplicationMode ReplicationMode;

    public Replicable(ReplicationMode mode, EventHandler<ReplicationEvent> incoming)
    {
        ReplicationMode = mode;
        IncomingReplication += incoming;
    }

    public event EventHandler<ReplicationEvent> IncomingReplication;

    protected virtual void OnIncomingReplication(ReplicationEvent e)
    {
        IncomingReplication?.Invoke(this, e);
    }
}