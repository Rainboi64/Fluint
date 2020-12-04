﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Fluint.Layer
{
    [AttributeUsage(AttributeTargets.Interface)]
    internal class InitializationAttribute : Attribute
    {
        public readonly InitializationMethod InitializationMethod;
        public InitializationAttribute(InitializationMethod method)
        {
            InitializationMethod = method;
        }
    }
}
