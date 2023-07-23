//
// InitializationAttribute.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;

namespace Fluint.Layer;

[AttributeUsage(AttributeTargets.Interface)]
internal class InitializationAttribute : Attribute
{
    public readonly InitializationMethod InitializationMethod;

    public InitializationAttribute(InitializationMethod method)
    {
        InitializationMethod = method;
    }
}