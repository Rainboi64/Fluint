﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Fluint.Layer.Miscellaneous
{
    public static class FastActivator
    {
        public static T CreateInstance<T>() where T : new()
        {
            return FastActivatorImpl<T>.Create();
        }

        private static class FastActivatorImpl<T> where T : new()
        {
            public static readonly Func<T> Create =
                DynamicModuleLambdaCompiler.GenerateFactory<T>();
        }
    }
}
