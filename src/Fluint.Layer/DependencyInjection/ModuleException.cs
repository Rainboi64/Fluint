// 
// ModuleException.cs
// 
// Copyright (C) 2022 Yaman Alhalabi

using System;

namespace Fluint.Layer.DependencyInjection;

public class ModuleException : Exception
{
    public ModuleException(Type abstraction) : base(
        $"an implementation module for {abstraction.FullName} was not able to be located, or it does not have constructor.")
    {
    }
}