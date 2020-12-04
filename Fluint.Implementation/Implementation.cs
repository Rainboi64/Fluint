using System.Collections;
using System;
using Fluint.Layer;
using System.Collections.Generic;
using Fluint.Implementation.Configuration;
using Fluint.Implementation.Debugging;

namespace Fluint.Implementation
{
    public class Implementation
    {
        private static readonly Type[] Modules = {
            typeof(SerilogLogger),
            typeof(ConfigurationManager)
        };

        public IEnumerable<Type> GetImplementations()
        {
            return Modules;
        }
    }
}