//
// InputBindingsConfiguration.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System.Collections.Generic;
using Fluint.Layer.Configuration;
using Fluint.Layer.Input;

namespace Fluint.Input.Base;

public struct InputBindingsConfiguration : IConfiguration
{
    public InputBindingsConfiguration(Dictionary<string, List<Binding>> bindings)
    {
        Bindings = bindings;
    }

    public Dictionary<string, List<Binding>> Bindings
    {
        get;
    }
}