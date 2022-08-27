// 
// StateException.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using System;

namespace Fluint.Layer.StateManagement;

public class StateException : Exception
{
    public StateException(string message) : base(message)
    {
    }
}