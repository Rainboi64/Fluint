//
// InputBindingsConfiguration.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using Fluint.Layer.Configuration;
using Fluint.Layer.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fluint.Implementation.Input
{
    public struct InputBindingsConfiguration : IConfiguration
    {
        public InputBindingsConfiguration(Dictionary<string, List<Binding>> bindings)
        {
            Bindings = bindings;
        }

        public Dictionary<string, List<Binding>> Bindings { get; }
    }
}
